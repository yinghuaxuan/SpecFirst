using System;

namespace DecisionMarkd.Serialization
{
    public class StringSerializer
    {
        public string Serialize(object data)
        {
            string result = null;
            if (string.Equals(data.ToString(), "null", StringComparison.OrdinalIgnoreCase))
            {
                result = $"{data.ToString().ToLowerInvariant()}, ";
            }
            else
            {
                result = $"\"{data.ToString()}\", ";
            }

            return result;
        }
    }
}
