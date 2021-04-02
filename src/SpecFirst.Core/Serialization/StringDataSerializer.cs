namespace SpecFirst.Core.Serialization
{
    using System;

    public class StringDataSerializer : IDataSerializer
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
                result = $"\"{data.ToString().Replace("\n", " ").Replace("\r", " ").Replace(@"""", @"\""")}\"";
            }

            return result;
        }
    }
}
