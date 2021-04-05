namespace SpecFirst.Core.TypeResolver
{
    using System;
    using System.Globalization;

    public class NumberDecisionTree
    {
        public static TypeDecisionNode Construct()
        {
            var root = DigitNode();

            var anyNode = AnyNode();
            var dotNode = DotNode();
            var eNode = ENode();
            var mNode = MNode();
            var dNode = DNode();
            var minusNode = MinusNode();
            root.ChildNodes.Add(mNode);
            root.ChildNodes.Add(dNode);
            root.ChildNodes.Add(eNode);
            root.ChildNodes.Add(dotNode);
            root.ChildNodes.Add(minusNode);
            root.ChildNodes.Add(anyNode);

            ConstructNumberDecisionTreeAfterDot(dotNode);
            ConstructNumberDecisionTreeAfterE(eNode);
            ConstructDateTimeDecisionTreeAfterMinus(minusNode);

            return root;
        }
        
        public static TypeDecisionNode ConstructPositiveNumberDecisionTree(TypeDecisionNode numberDecisionTree)
        {
            var node = PlusNode();
            var anyNode = AnyNode();
            node.ChildNodes.Add(numberDecisionTree);
            node.ChildNodes.Add(anyNode);
            return node;
        }

        public static TypeDecisionNode ConstructNegativeNumberDecisionTree(TypeDecisionNode numberDecisionTree)
        {
            var node = MinusNode();
            var anyNode = AnyNode();
            node.ChildNodes.Add(numberDecisionTree);
            node.ChildNodes.Add(anyNode);
            return node;
        }

        private static void ConstructDateTimeDecisionTreeAfterMinus(TypeDecisionNode minusNode)
        {
            var digitAfterMinusNode = DigitAfterMinusNode();
            var anyNode = AnyNode();
            minusNode.ChildNodes.Add(digitAfterMinusNode);
            minusNode.ChildNodes.Add(anyNode);

            var secondMinusNode = MinusNode();
            digitAfterMinusNode.ChildNodes.Add(secondMinusNode);
            digitAfterMinusNode.ChildNodes.Add(anyNode);

            var secondDigitAfterMinusNode = DigitAfterMinusNode();
            secondMinusNode.ChildNodes.Add(secondDigitAfterMinusNode);
            secondMinusNode.ChildNodes.Add(anyNode);

            var spaceNode = SpaceNode();
            secondDigitAfterMinusNode.ChildNodes.Add(spaceNode);
            secondDigitAfterMinusNode.ChildNodes.Add(anyNode);

            var digitAfterSpaceNode = DigitAfterMinusNode();
            spaceNode.ChildNodes.Add(digitAfterSpaceNode);
            spaceNode.ChildNodes.Add(anyNode);

            var colonNode = ColonNode();
            digitAfterSpaceNode.ChildNodes.Add(colonNode);
            digitAfterSpaceNode.ChildNodes.Add(anyNode);

            var digitAfterColonNode = DigitAfterMinusNode();
            colonNode.ChildNodes.Add(digitAfterColonNode);
            colonNode.ChildNodes.Add(anyNode);

            var secondColonNode = ColonNode();
            digitAfterColonNode.ChildNodes.Add(secondColonNode);
            digitAfterColonNode.ChildNodes.Add(anyNode);

            var secondDigitAfterColonNode = DigitAfterMinusNode();
            secondColonNode.ChildNodes.Add(secondDigitAfterColonNode);
            secondColonNode.ChildNodes.Add(anyNode);
        }

        private static void ConstructNumberDecisionTreeAfterDot(TypeDecisionNode dotNode)
        {
            var digitAfterDotNode = DigitAfterDotNode();
            var anyNode = AnyNode();
            dotNode.ChildNodes.Add(digitAfterDotNode);
            dotNode.ChildNodes.Add(anyNode);

            var eNodeAfterDot = ENode();
            var mNodeAfterDot = MNode();
            var dNodeAfterDot = DNode();
            digitAfterDotNode.ChildNodes.Add(mNodeAfterDot);
            digitAfterDotNode.ChildNodes.Add(dNodeAfterDot);
            digitAfterDotNode.ChildNodes.Add(eNodeAfterDot);
            digitAfterDotNode.ChildNodes.Add(anyNode);

            ConstructNumberDecisionTreeAfterE(eNodeAfterDot);
        }

        private static void ConstructNumberDecisionTreeAfterE(TypeDecisionNode theENode)
        {
            var minusAfterENode = MinusNode();
            var plusAfterENode = PlusNode();
            var anyNode = AnyNode();
            theENode.ChildNodes.Add(minusAfterENode);
            theENode.ChildNodes.Add(plusAfterENode);
            theENode.ChildNodes.Add(anyNode);

            var digitAfterENode = DigitNode();
            minusAfterENode.ChildNodes.Add(digitAfterENode);
            minusAfterENode.ChildNodes.Add(anyNode);
            plusAfterENode.ChildNodes.Add(digitAfterENode);
            plusAfterENode.ChildNodes.Add(anyNode);

            var mNodeAfterDash = MNode();
            var dNodeAfterDash = DNode();
            digitAfterENode.ChildNodes.Add(mNodeAfterDash);
            digitAfterENode.ChildNodes.Add(dNodeAfterDash);
            digitAfterENode.ChildNodes.Add(anyNode);
        }

        private static TypeDecisionNode DigitAfterDotNode()
        {
            return new TypeDecisionNode
            {
                ShouldProcess = c => char.IsDigit(c),
                NodeType = (s) =>
                {
                    if (double.TryParse(s, out var d)) return new TypeValuePair(typeof(double), d);
                    return new TypeValuePair(typeof(string), s);
                }
            };
        }

        private static TypeDecisionNode DigitAfterMinusNode()
        {
            return new TypeDecisionNode
            {
                ShouldProcess = c => char.IsDigit(c),
                NodeType = (s) =>
                {
                    if (DateTime.TryParseExact(s, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out var d)) return new TypeValuePair(typeof(DateTime), d);
                    return new TypeValuePair(typeof(string), s);
                }
            };
        }

        private static TypeDecisionNode DigitNode()
        {
            var node = new TypeDecisionNode();
            node.ShouldProcess = c => { return char.IsDigit(c); };
            node.NodeType = (s) =>
            {
                if (int.TryParse(s, NumberStyles.Any, CultureInfo.InvariantCulture, out var i)) return new TypeValuePair(typeof(int), i);
                if (double.TryParse(s, NumberStyles.Any, CultureInfo.InvariantCulture, out var d)) return new TypeValuePair(typeof(double), d);
                return new TypeValuePair(typeof(string), s);
            };
            return node;
        }

        private static TypeDecisionNode DotNode()
        {
            return new TypeDecisionNode
            {
                ShouldProcess = c => c == '.',
                NodeType = (s) => new TypeValuePair(typeof(string), s)
            };
        }

        private static TypeDecisionNode ENode()
        {
            return new TypeDecisionNode
            {
                ShouldProcess = c => c == 'E' || c == 'e',
                NodeType = (s) => new TypeValuePair(typeof(string), s)
            };
        }

        private static TypeDecisionNode MNode()
        {
            return new TypeDecisionNode
            {
                ShouldProcess = c => c == 'M' || c == 'm',
                NodeType = (s) => 
                {
                    if (decimal.TryParse(s.TrimEnd('M', 'm'), NumberStyles.Any, CultureInfo.InvariantCulture, out var m)) return new TypeValuePair(typeof(decimal), m);
                    return new TypeValuePair(typeof(string), s);
                }
            };
        }

        private static TypeDecisionNode DNode()
        {
            return new TypeDecisionNode
            {
                ShouldProcess = c => c == 'D' || c == 'd',
                NodeType = (s) =>
                {
                    if (double.TryParse(s.TrimEnd('D', 'd'), out var d)) return new TypeValuePair(typeof(double), d);
                    return new TypeValuePair(typeof(string), s);
                }
            };
        }

        private static TypeDecisionNode MinusNode()
        {
            return new TypeDecisionNode
            {
                ShouldProcess = c => c == '-',
                NodeType = (s) => new TypeValuePair(typeof(string), s)
            };
        }

        private static TypeDecisionNode PlusNode()
        {
            return new TypeDecisionNode
            {
                ShouldProcess = c => c == '+',
                NodeType = (s) => new TypeValuePair(typeof(string), s)
            };
        }

        private static TypeDecisionNode SpaceNode()
        {
            return new TypeDecisionNode
            {
                ShouldProcess = c => c == ' ',
                NodeType = (s) => new TypeValuePair(typeof(string), s)
            };
        }

        private static TypeDecisionNode ColonNode()
        {
            return new TypeDecisionNode
            {
                ShouldProcess = c => c == ':',
                NodeType = (s) => new TypeValuePair(typeof(string), s)
            };
        }

        private static TypeDecisionNode AnyNode()
        {
            return new TypeDecisionNode
            {
                ShouldProcess = c => true,
                NodeType = (s) => new TypeValuePair(typeof(string), s)
            };
        }
    }
}
