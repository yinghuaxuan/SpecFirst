using System;

namespace DecisionMarkd.Serialization
{
    public class DateTimeDataSerializer
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
                DateTime date = (DateTime)data;
                result = $"new DateTime({date.Year}, {date.Month}, {date.Day}), ";
            }

            return result;
        }
    }
}
