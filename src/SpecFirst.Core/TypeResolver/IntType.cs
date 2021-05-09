namespace SpecFirst.Core.TypeResolver
{
    public class IntType
    {
        public IntType(string originalValue, int parsedValue)
        {
            OriginalValue = originalValue;
            ParsedValue = parsedValue;
        }

        public string OriginalValue { get; }
        public int ParsedValue { get; }
    }
}
