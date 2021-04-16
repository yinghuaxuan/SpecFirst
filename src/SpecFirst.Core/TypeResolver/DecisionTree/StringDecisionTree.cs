namespace SpecFirst.Core.TypeResolver
{
    public class StringDecisionTree
    {
        public static TypeDecisionNode Construct()
        {
            return StringNode();
        }

        private static TypeDecisionNode StringNode()
        {
            return new TypeDecisionNode
            {
                ShouldProcess = c => true,
                NodeType = (s) => new TypeValuePair(typeof(string), s)
            };
        }
    }
}
