namespace SpecFirst.Core.TypeResolver
{
    public class BooleanDecisionTree
    {
        public static TypeDecisionNode ConstructTrueTree()
        {
            var tNode = TNode();
            var anyNode = AnyNode();

            var rNode = RNode();
            tNode.ChildNodes.Add(rNode);
            tNode.ChildNodes.Add(anyNode);

            var uNode = UNode();
            rNode.ChildNodes.Add(uNode);
            rNode.ChildNodes.Add(anyNode);

            var eNode = ENode();
            uNode.ChildNodes.Add(eNode);
            uNode.ChildNodes.Add(anyNode);

            return tNode;
        }

        public static TypeDecisionNode ConstructFalseTree()
        {
            var fNode = FNode();
            var anyNode = AnyNode();

            var aNode = ANode();
            fNode.ChildNodes.Add(aNode);
            fNode.ChildNodes.Add(anyNode);

            var lNode = LNode();
            aNode.ChildNodes.Add(lNode);
            aNode.ChildNodes.Add(anyNode);

            var sNode = SNode();
            lNode.ChildNodes.Add(sNode);
            lNode.ChildNodes.Add(anyNode);

            var eNode = ENode();
            sNode.ChildNodes.Add(eNode);
            sNode.ChildNodes.Add(anyNode);

            return fNode;
        }

        private static TypeDecisionNode TNode()
        {
            return new TypeDecisionNode
            {
                ShouldProcess = c => c == 'T' || c == 't',
                NodeType = (s) =>
                {
                    return new TypeValuePair(typeof(string), s);
                }
            };
        }

        private static TypeDecisionNode RNode()
        {
            return new TypeDecisionNode
            {
                ShouldProcess = c => c == 'R' || c == 'r',
                NodeType = (s) =>
                {
                    return new TypeValuePair(typeof(string), s);
                }
            };
        }

        private static TypeDecisionNode UNode()
        {
            return new TypeDecisionNode
            {
                ShouldProcess = c => c == 'U' || c == 'u',
                NodeType = (s) =>
                {
                    return new TypeValuePair(typeof(string), s);
                }
            };
        }

        private static TypeDecisionNode ENode()
        {
            return new TypeDecisionNode
            {
                ShouldProcess = c => c == 'E' || c == 'e',
                NodeType = (s) =>
                {
                    if (bool.TryParse(s.ToLower(), out var b)) return new TypeValuePair(typeof(bool), b);
                    return new TypeValuePair(typeof(string), s);
                }
            };
        }

        private static TypeDecisionNode FNode()
        {
            return new TypeDecisionNode
            {
                ShouldProcess = c => c == 'F' || c == 'f',
                NodeType = (s) =>
                {
                    return new TypeValuePair(typeof(string), s);
                }
            };
        }

        private static TypeDecisionNode ANode()
        {
            return new TypeDecisionNode
            {
                ShouldProcess = c => c == 'A' || c == 'a',
                NodeType = (s) =>
                {
                    return new TypeValuePair(typeof(string), s);
                }
            };
        }

        private static TypeDecisionNode LNode()
        {
            return new TypeDecisionNode
            {
                ShouldProcess = c => c == 'L' || c == 'l',
                NodeType = (s) =>
                {
                    return new TypeValuePair(typeof(string), s);
                }
            };
        }

        private static TypeDecisionNode SNode()
        {
            return new TypeDecisionNode
            {
                ShouldProcess = c => c == 'S' || c == 's',
                NodeType = (s) =>
                {
                    return new TypeValuePair(typeof(string), s);
                }
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
