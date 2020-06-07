using System;

namespace DecisionMarkd.Serialization
{
    public class StringDataSerializer
    {
        public string Serialize(object data)
        {
            string result;
            if (string.Equals(data.ToString(), "null", StringComparison.OrdinalIgnoreCase))
            {
                result = $"{data.ToString().ToLowerInvariant()}, ";
            }
            else if (data is DateTime date)
            {
                result = $"\"{date:yyyy-MM-dd HH:mm:ss}\", ";
            }
            else if(data is bool b)
            {
                result = $"\"{b.ToString().ToLowerInvariant()}\", ";
            }
            else
            {
                result = $"\"{data.ToString().Replace("\n", " ").Replace("\r", " ").Replace(@"""", @"\""")}\", ";
            }

            return result;
        }
    }
}
