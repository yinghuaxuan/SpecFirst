namespace SpecFirst.Core.TypeResolver
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public static class CollectionTypeResolver
    {
        /// <summary>
        /// Return the most compatible type of the collection
        /// </summary>
        /// <param name="types">An array of types.</param>
        /// <returns>The most compatible type of all types.</returns>
        public static Type Resolve(Type[] types)
        {
            bool isArrayType = types.All(t => t.IsArray);

            return ResolveType(types, isArrayType);
        }

        private static Type ResolveType(Type[] types, bool isArrayType)
        {
            var hintType = types[0];
            for (int i = 1; i < types.Length; i++)
            {
                if (types[i] != hintType)
                {
                    hintType = isArrayType ? FindMostCompatibleArrayType(hintType, types[i]) : FindMostCompatibleType(hintType, types[i]);
                }
            }

            return hintType;

            Type FindMostCompatibleType(Type type1, Type type2)
            {
                if (type1 == typeof(IntType) && type2 == typeof(DoubleType) || type2 == typeof(IntType) && type1 == typeof(DoubleType))
                {
                    return typeof(DoubleType);
                }

                if (type1 == typeof(IntType) && type2 == typeof(DecimalType) || type2 == typeof(IntType) && type1 == typeof(DecimalType))
                {
                    return typeof(DecimalType);
                }

                return typeof(object);
            }

            Type FindMostCompatibleArrayType(Type type1, Type type2)
            {
                if (type1 == typeof(IntType[]) && type2 == typeof(DoubleType[]) || type2 == typeof(IntType[]) && type1 == typeof(DoubleType[]))
                {
                    return typeof(double[]);
                }

                if (type1 == typeof(IntType[]) && type2 == typeof(DecimalType[]) || type2 == typeof(IntType[]) && type1 == typeof(DecimalType[]))
                {
                    return typeof(DecimalType[]);
                }

                return typeof(object[]);
            }
        }

        /// <summary>
        /// Return the most compatible type for the collection
        /// </summary>
        /// <param name="collectionString">Collection in a string format, e.g. [1, 2, 3, 4].</param>
        /// <returns>The most compatible type of all types.</returns>
        public static Type Resolve(string collectionString, out object value)
        {
            value = null;

            bool isCollection = TryGetCollectionSegments(collectionString, out var segments);
            if (!isCollection)
            {
                value = collectionString;
                return typeof(string);
            }

            List<Type> types = new List<Type>();
            List<object> parsedValues = new List<object>();
            foreach (var segment in segments)
            {
                var type = TypeResolver.Resolve(segment, out var parsedValue);
                types.Add(type);
                parsedValues.Add(parsedValue);
            }

            value = parsedValues.ToArray();
            var collectionType = Resolve(types.ToArray());
            return collectionType.MakeArrayType();
        }

        private static bool TryGetCollectionSegments(string collectionString, out List<string> segments)
        {
            segments = new List<string>();
            var stack = new Stack<char>();
            stack.Push(collectionString[0]);
            var previousChar = collectionString[0];
            for (int i = 1; i < collectionString.Length; i++)
            {
                if (collectionString[i] == ',')
                {
                    if (previousChar != '\\')
                    {
                        if (stack.Count == 0 || stack.Count == 1 && stack.Peek() == '[')
                        {
                            segments.Clear();
                            return false;
                        }

                        var builder = new StringBuilder();
                        while (stack.Peek() != '[')
                        {
                            builder.Append(stack.Pop());
                        }

                        segments.Add(new string(builder.ToString().Reverse().ToArray()));
                    }
                    else
                    {
                        stack.Push(collectionString[i]);
                    }
                }
                else if (collectionString[i] == ']')
                {
                    if (previousChar != '\\')
                    {
                        if (stack.Count == 0)
                        {
                            segments.Clear();
                            return false;
                        }

                        var builder = new StringBuilder();
                        while (stack.Peek() != '[')
                        {
                            builder.Append(stack.Pop());
                        }

                        if (stack.Peek() == '[')
                        {
                            stack.Pop();
                        }
                        else
                        {
                            segments.Clear();
                            return false;
                        }

                        segments.Add(new string(builder.ToString().Reverse().ToArray()));
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

            if (stack.Count > 0)
            {
                segments.Clear();
                return false;
            }

            return true;
        }
    }
}
