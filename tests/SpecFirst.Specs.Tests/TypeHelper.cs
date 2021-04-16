namespace SpecFirst.Specs.Tests
{
    using System;
    using System.Text.RegularExpressions;
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

        public static string Serialize(object data)
        {
            string value = data switch
            {
                NumberValue _ => new NumberDataSerializer().Serialize(data),
                DateTime _ => new DateTimeDataSerializer().Serialize(data),
                bool _ => new BooleanDataSerializer().Serialize(data),
                string _ => Regex.Unescape(new StringDataSerializer().Serialize(data).ReplaceFirstOccurrence("\"", string.Empty).ReplaceLastOccurrence("\"", string.Empty)),
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