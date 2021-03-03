namespace SpecFirst.TestsGenerator
{
    using SpecFirst.Core.DecisionTable;

    public interface ITestsGenerator
    {
        string[] Generate(string namespaceName, DecisionTable[] decisionTables);
    }
}