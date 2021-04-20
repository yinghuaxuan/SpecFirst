namespace SpecFirst.Core.TypeResolver
{
    public class DecimalType
    {
        public DecimalType(string originalValue, decimal parsedValue)
        {
            OriginalValue = originalValue;
            ParsedValue = parsedValue;
        }

        public string OriginalValue { get; }
        public decimal ParsedValue { get; }
    }
}
