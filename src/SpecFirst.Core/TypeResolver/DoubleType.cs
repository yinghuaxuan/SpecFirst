namespace SpecFirst.Core.TypeResolver
{
    public class DoubleType
    {
        public DoubleType(string originalValue, double parsedValue)
        {
            OriginalValue = originalValue;
            ParsedValue = parsedValue;
        }

        public string OriginalValue { get; }
        public double ParsedValue { get; }
    }
}
