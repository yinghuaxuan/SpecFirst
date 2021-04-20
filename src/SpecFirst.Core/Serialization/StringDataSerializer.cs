namespace SpecFirst.Core.Serialization
{
    using System;

    public class StringDataSerializer : IPrimitiveDataSerializer
    {
        public string Serialize(object data)
        {
            string result;
            if (string.Equals(data.ToString(), "null", StringComparison.OrdinalIgnoreCase))
            {
                result = $"{data.ToString().ToLowerInvariant()}";
            }
            else
            {
                result = $"\"{SanitizeString(data.ToString())}\"";
            }

            return result;
        }

        private static string SanitizeString(string value)
        {
            if (value.StartsWith("\"") && value.EndsWith("\""))
            {
                value = value.TrimFirst("\"").TrimLast("\"");
            }

            return 
                value
                .Replace("\n", " ")
                .Replace("\r", " ")
                //.Replace(@"""", @"\""")
                .Replace("\\", "\\\\")
                .Replace("\"", "\\\"")
                ;
        }
    }

    public static class StringExtension
    {
        public static string TrimFirst(this string source, string trim)
        {
            int place = source.IndexOf(trim, StringComparison.Ordinal);
            string result = source.Remove(place, trim.Length);
            return result;
        }

        public static string TrimLast(this string source, string trim)
        {
            int place = source.LastIndexOf(trim, StringComparison.Ordinal);
            string result = source.Remove(place, trim.Length);
            return result;
        }
    }
}
