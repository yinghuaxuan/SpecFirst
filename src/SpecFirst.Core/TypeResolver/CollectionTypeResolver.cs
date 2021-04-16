namespace SpecFirst.Core.TypeResolver
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class CollectionTypeResolver
    {
        /// <summary>
        /// Return the most compatible type of the collection
        /// </summary>
        /// <param name="types">An array of types.</param>
        /// <returns>The most compatible type of all types.</returns>
        public static Type Resolve(Type[] types)
        {
            var hintType = types[0];
            for (int i = 1; i < types.Length; i++)
            {
                if (types[i] != hintType)
                {
                    if (hintType == typeof(int) && (types[i] == typeof(double) || types[i] == typeof(decimal)))
                    {
                        hintType = types[i];
                    }
                    else
                    {
                        return typeof(object);
                    }
                }
            }

            return hintType;
        }

        /// <summary>
        /// Return the most compatible type for the collection
        /// </summary>
        /// <param name="collectionString">Collection in a string format, e.g. [1, 2, 3, 4].</param>
        /// <returns>The most compatible type of all types.</returns>
        public static Type Resolve(string collectionString, out object[] values)
        {
            values = null;

            var segments = GetCollectionSegments(collectionString);

            List<Type> types = new List<Type>();
            List<object> parsedValues = new List<object>();
            foreach (var segment in segments)
            {
                var type = ScalaValueTypeResolver.Resolve(segment, out var parsedValue);
                types.Add(type);
                parsedValues.Add(parsedValue);
            }

            values = parsedValues.ToArray();
            var collectionType = Resolve(types.ToArray());
            return collectionType.MakeArrayType();
        }

        private static List<string> GetCollectionSegments(string collectionString)
        {
            List<string> segments = new List<string>();
            var stack = new Stack<char>();
            stack.Push(collectionString[0]);
            var previousChar = collectionString[0];
            for (int i = 1; i < collectionString.Length; i++)
            {
                if (collectionString[i] == ',' || collectionString[i] == ']')
                {
                    if (previousChar != '\\')
                    {
                        var builder = new StringBuilder();
                        while (stack.Peek() != ']')
                        {
                            builder.Append(stack.Pop());
                        }

                        segments.Add(builder.ToString());
                    }
                    else
                    {
                        stack.Push(collectionString[i]);
                    }
                }
                else
                {
                    stack.Push(collectionString[i]);
                }

                previousChar = collectionString[i];
            }

            return segments;
        }
    }
}
