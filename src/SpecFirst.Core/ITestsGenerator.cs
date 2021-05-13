namespace SpecFirst.Core
{
    using System.Collections.Generic;
    using SpecFirst.Core.Setting;

    public interface ITestsGenerator
    {
        IEnumerable<string> Generate(SpecFirstSettings settings, IEnumerable<DecisionTable.DecisionTable> decisionTables);
    }
}