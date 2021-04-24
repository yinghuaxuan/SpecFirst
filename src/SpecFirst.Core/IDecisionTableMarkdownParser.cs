namespace SpecFirst.Core
{
    using System.Collections.Generic;

    public interface IDecisionTableMarkdownParser
    {
        List<DecisionTable.DecisionTable> Parse(string markdownText);
    }
}