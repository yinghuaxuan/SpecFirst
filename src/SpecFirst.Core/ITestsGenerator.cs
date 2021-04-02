namespace SpecFirst.Core
{
    public interface ITestsGenerator
    {
        string[] Generate(string namespaceName, DecisionTable.DecisionTable[] decisionTables);
    }
}