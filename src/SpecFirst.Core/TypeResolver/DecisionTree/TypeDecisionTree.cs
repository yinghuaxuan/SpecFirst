namespace SpecFirst.Core.TypeResolver
{
    public class TypeDecisionTree
    {
        private TypeDecisionNode _root;

        public TypeDecisionTree()
        {
            ConstructDecisionTree();
        }

        public TypeDecisionNode Root => _root;


        private void ConstructDecisionTree()
        {
            _root = new TypeDecisionNode
            {
                ShouldProcess = c => false,
                NodeType = (s) => null
            };

            var numberDecisionTree = NumberDecisionTree.Construct();
            var positiveNumberDecisionTree = NumberDecisionTree.ConstructPositiveNumberDecisionTree(numberDecisionTree);
            var negativeNumberDecisionTree = NumberDecisionTree.ConstructNegativeNumberDecisionTree(numberDecisionTree);
            var trueDecisionTree = BooleanDecisionTree.ConstructTrueTree();
            var falseDecisionTree = BooleanDecisionTree.ConstructFalseTree();
            _root.ChildNodes.Add(numberDecisionTree);
            _root.ChildNodes.Add(positiveNumberDecisionTree);
            _root.ChildNodes.Add(negativeNumberDecisionTree);
            _root.ChildNodes.Add(trueDecisionTree);
            _root.ChildNodes.Add(falseDecisionTree);
            _root.ChildNodes.Add(AnyNode());
        }

        private static TypeDecisionNode AnyNode()
        {
            return new TypeDecisionNode
            {
                ShouldProcess = c => true,
                NodeType = (s) => new TypeValuePair(typeof(string), s.Trim('\"'))
            };
        }
    }
}
