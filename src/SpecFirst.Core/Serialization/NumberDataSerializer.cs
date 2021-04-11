namespace SpecFirst.Core.Serialization
{
    using System;
    using System.Diagnostics;

    public class NumberDataSerializer : IDataSerializer
    {
        public string Serialize(object data)
        {
            var value = data as NumberValue;

            Debug.Assert(!(value is null));

            if (value.ParsedValue is double && !value.OriginalValue.EndsWith("D", StringComparison.OrdinalIgnoreCase))
            {
                return $"{value.OriginalValue}D";
            }

            return value.OriginalValue.ToUpper().TrimStart('+');
        }
    }
}
