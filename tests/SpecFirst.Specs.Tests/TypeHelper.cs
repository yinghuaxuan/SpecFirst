namespace SpecFirst.Specs.Tests
{
    using System;
    using System.Linq;
    using System.Text.RegularExpressions;
    using SpecFirst.Core.TypeResolver;
    using SpecFirst.xUnit.Serialization;

    public static class TypeHelper
    {
        public static string GetTypeString(Type type)
        {
            if (type.IsArray) return GetPrimitiveTypeString(type.GetElementType());
            return GetPrimitiveTypeString(type);
        }

        private static string GetPrimitiveTypeString(Type type)
        {
            if (type == typeof(int)) return "integer";
            if (type == typeof(IntType)) return "integer";
            if (type == typeof(decimal)) return "decimal";
            if (type == typeof(DecimalType)) return "decimal";
            if (type == typeof(double)) return "double";
            if (type == typeof(DoubleType)) return "double";
            if (type == typeof(string)) return "string";
            if (type == typeof(DateTime)) return "datetime";
            if (type == typeof(bool)) return "bool";
            if (type == typeof(object)) return "object";
            throw new InvalidOperationException();
        }

        public static string Serialize(object data, Type type)
        {
            var numberSerializer = new NumberDataSerializer();
            var datetimeSerializer = new DateTimeDataSerializer();
            var booleanSerializer = new BooleanDataSerializer();
            var stringSerializer = new StringDataSerializer();
            var arraySerializer = new ArrayDataSerializer(stringSerializer, numberSerializer, datetimeSerializer, booleanSerializer);

            if (data.GetType().IsArray)
            {
                var serialized = arraySerializer.Serialize(data, type);
                if (type == typeof(string[])) // we need to unescape string
                {
                    var firstIndex = serialized.IndexOf("{", StringComparison.Ordinal);
                    var lastIndex = serialized.LastIndexOf("}", StringComparison.Ordinal);
                    var arrayContent = serialized.Substring(firstIndex + 1, lastIndex - firstIndex - 1);
                    var splittedContent = arrayContent.Split(",");
                    var joinedContent = string.Join(",", splittedContent.Select(c => Regex.Unescape(c)));
                    return $"{serialized.Substring(0, firstIndex)}{{{joinedContent}}}";
                }

                return serialized;
            }

            string value = data switch
            {
                IntType _ => numberSerializer.Serialize(data),
                DoubleType _ => numberSerializer.Serialize(data),
                DecimalType _ => numberSerializer.Serialize(data),
                DateTime _ => datetimeSerializer.Serialize(data),
                bool _ => booleanSerializer.Serialize(data),
                string _ => Regex.Unescape(stringSerializer.Serialize(data).ReplaceFirstOccurrence("\"", string.Empty).ReplaceLastOccurrence("\"", string.Empty)),
                _ => throw new InvalidOperationException()
            };

            return value;
        }
    }

    public static class StringExtension
    {
        public static string ReplaceFirstOccurrence(this string source, string find, string replace)
        {
            int place = source.IndexOf(find, StringComparison.Ordinal);
            string result = source.Remove(place, find.Length).Insert(place, replace);
            return result;
        }

        public static string ReplaceLastOccurrence(this string source, string find, string replace)
        {
            int place = source.LastIndexOf(find, StringComparison.Ordinal);
            string result = source.Remove(place, find.Length).Insert(place, replace);
            return result;
        }
    }
}