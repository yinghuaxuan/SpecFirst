namespace SpecFirst.Core.TypeResolver
{
    using System;
    using System.Linq;

    public class ScalaValueTypeResolver
    {
        private static readonly TypeDecisionTree _typeDecisionTree;

        static ScalaValueTypeResolver()
        {
            _typeDecisionTree = new TypeDecisionTree();
        }

        /// <summary>
        /// Supported data types: int, decimal, date, boolean, string, array,
        /// Special keywords: null, blank
        /// </summary>
        /// <param name="value">The value in string.</param>
        /// <param name="parsedValue">The value in its type.</param>
        /// <returns>The real type of the value.</returns>
        public static Type Resolve(string value, out object parsedValue)
        {
            parsedValue = null;
            TypeValuePair typeValuePair;
            value = string.IsNullOrWhiteSpace(value) ? value : value.Trim();
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
