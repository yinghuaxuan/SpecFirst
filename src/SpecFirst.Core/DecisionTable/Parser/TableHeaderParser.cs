namespace SpecFirst.Core.DecisionTable.Parser
{
    public class TableHeaderParser
    {
        public TableHeader Parse(string header)
        {
            ReadOnlySpan<char> headerSpan = header.AsSpan();

            Span<char> variableName = new char[header.Length];
            int length;
            TableHeaderType direction;
            if (headerSpan.StartsWith("#".AsSpan()))
            {
                length = headerSpan.Slice(1, headerSpan.Length - 1).ToLowerInvariant(variableName);
                direction = TableHeaderType.Comment;
            }
            else if (headerSpan.EndsWith("?".AsSpan()))
            {
                length = headerSpan.Slice(0, headerSpan.Length - 1).ToLowerInvariant(variableName);
                direction = TableHeaderType.Output;
            }
            else
            {
                length = headerSpan.ToLowerInvariant(variableName);
                direction = TableHeaderType.Input;
            }

            return new TableHeader(variableName.Slice(0, length).ToString(), direction);
        }
    }
}