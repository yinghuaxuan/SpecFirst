namespace SpecFirst.Core.TypeResolver
{
    using System;

    public static class TypeResolver
    {
        public static Type Resolve(string value, out object parsedValue)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                parsedValue = value;
                return typeof(string);
            }

            value = value.Trim();
            Type type;
            if (value.StartsWith("["))
            {
                type = CollectionTypeResolver.Resolve(value, out var scalaValue);
                parsedValue = scalaValue;
            }
            else
            {
                type = ScalaValueTypeResolver.Resolve(value, out var collectionValue);
                parsedValue = collectionValue;
            }

            return type;
        }
    }
}
