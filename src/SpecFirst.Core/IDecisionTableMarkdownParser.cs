namespace SpecFirst.Core
{
    using System.Collections.Generic;

    public interface IDecisionTableMarkdownParser
    {
        IEnumerable<DecisionTable.DecisionTable> Parse(string markdownText);
    }
}