namespace SpecFirst.TestsGenerator.xUnit.Serialization
{
    using System;
    using SpecFirst.Core.TypeResolver;

    public class NumberDataSerializer : IPrimitiveDataSerializer
    {
        public string Serialize(object data)
        {
            switch (data)
            {
                case IntType value:
                    return SerializeInt(value);
                case DecimalType value:
                    return SerializeDecimal(value);
                case DoubleType value:
                    return SerializeDouble(value);
                default:
                    throw new InvalidOperationException();
            }
        }

        private string SerializeDouble(DoubleType value)
        {
            if (!value.OriginalValue.EndsWith("D", StringComparison.OrdinalIgnoreCase))
            {
                return $"{value.OriginalValue}D";
            }

            return value.OriginalValue.ToUpper().TrimStart('+');
        }

        private string SerializeInt(IntType value)
        {
            return value.OriginalValue.ToUpper().TrimStart('+');
        }

        private string SerializeDecimal(DecimalType value)
        {
            return value.OriginalValue.ToUpper().TrimStart('+');
        }
    }
}
