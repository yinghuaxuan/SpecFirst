namespace SpecFirst.Core.TypeResolver
{
    using System;

    public class CollectionTypeResolver
    {
        /// <summary>
        /// Supported data types: int, decimal, date, boolean, string, array,
        /// Special keywords: null
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
    }
}
