namespace SpecFirst.Core
{
    public class NumberValue
    {
        public NumberValue(string originalValue, object parsedValue)
        {
            OriginalValue = originalValue;
            ParsedValue = parsedValue;
        }

        public string OriginalValue { get; }
        public object ParsedValue { get; }
    }
}
