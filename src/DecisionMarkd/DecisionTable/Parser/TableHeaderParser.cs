namespace DecisionMarkd.DecisionTable.Parser
{
    using System;

    public class TableHeaderParser
    {
        public DecisionVariable Convert(string header)
        {
            ReadOnlySpan<char> headerSpan = header.AsSpan();

            Span<char> variableName = new char[header.Length];
            int length;
            DecisionVariableType direction;
            if (headerSpan.StartsWith("#".AsSpan()))
            {
                length = headerSpan.Slice(1, headerSpan.Length - 1).ToLowerInvariant(variableName);
                direction = DecisionVariableType.Comment;
            }
            else if (headerSpan.EndsWith("?".AsSpan()))
            {
                length = headerSpan.Slice(0, headerSpan.Length - 1).ToLowerInvariant(variableName);
                direction = DecisionVariableType.Output;
            }
            else
            {
                length = headerSpan.ToLowerInvariant(variableName);
                direction = DecisionVariableType.Input;
            }

            return new DecisionVariable(variableName.Slice(0, length).ToString(), direction);
        }
    }
}