namespace SpecFirst.Specs.Tests
{
    using System;

    public class TypeHelper
    {
        public static string GetTypeString(Type type)
        {
            if (type == typeof(int)) return "integer";
            if (type == typeof(decimal)) return "decimal";
            if (type == typeof(double)) return "double";
            if (type == typeof(string)) return "string";
            if (type == typeof(DateTime)) return "datetime";
            throw new InvalidOperationException();
        }
    }
}