namespace SpecFirst.Core.TypeResolver
{
    using System;
    using System.Linq;

    public static class ScalaValueTypeResolver
    {
        private static readonly TypeDecisionTree _typeDecisionTree;

        static ScalaValueTypeResolver()
        {
            _typeDecisionTree = new TypeDecisionTree();
        }

        /// <summary>
        /// Infer the actual type from the text.
        /// Supported data types: int, decimal, double, date, boolean, string, array.
        /// </summary>
        /// <param name="value">The value in string.</param>
        /// <param name="parsedValue">The value in its type.</param>
        /// <returns>The real type of the value.</returns>
        public static Type Resolve(string value, out object parsedValue)
        {
            parsedValue = null;
            TypeValuePair typeValuePair;
            var root = _typeDecisionTree.Root;
            var currentNode = root;
            for (var index = 0; index < value.Length; index++)
            {
                var shouldProcess = currentNode.ShouldProcess(value[index]);
                if (!shouldProcess)
                {
                    if (currentNode.ChildNodes.Any())
                    {
                        foreach (var node in currentNode.ChildNodes)
                        {
                            if (node.ShouldProcess(value[index]))
                            {
                                currentNode = node;
                                break;
                            }
                        }
                    }
                    else
                    {
                        typeValuePair = currentNode.NodeType(value.Substring(0, index));
                        parsedValue = typeValuePair.Value;
                        return typeValuePair.Type;
                    }
                }
            }

            typeValuePair = currentNode.NodeType(value);
            parsedValue = typeValuePair.Value;
            return typeValuePair.Type;
        }
    }
}
