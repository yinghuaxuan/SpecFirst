namespace SpecFirst.Specs.Tests
{
    using System;
    using SpecFirst.Core.Serialization;
    using SpecFirst.Core.TypeResolver;

    public class TypeHelper
    {
        public static string GetTypeString(Type type)
        {
            if (type == typeof(int)) return "integer";
            if (type == typeof(decimal)) return "decimal";
            if (type == typeof(double)) return "double";
            if (type == typeof(string)) return "string";
            if (type == typeof(DateTime)) return "datetime";
            if (type == typeof(bool)) return "bool";
            throw new InvalidOperationException();
        }

        public static string Convert(object data)
        {
            string value = data switch
            {
                NumberValue _ => new NumberDataSerializer().Serialize(data),
                DateTime _ => new DateTimeDataSerializer().Serialize(data),
                bool _ => new BooleanDataSerializer().Serialize(data),
                string _ => new StringDataSerializer().Serialize(data).Replace("\"", string.Empty),
                _ => throw new InvalidOperationException()
            };

            return value;
        }
    }
}