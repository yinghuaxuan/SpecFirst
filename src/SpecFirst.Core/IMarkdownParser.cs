namespace SpecFirst.Core
{
    using System.Collections.Generic;

    public interface IMarkdownParser
    {
        List<DecisionTable.DecisionTable> Parse(string markdownText);
    }
}