namespace SpecFirst.Template.xUnit
{
    using SpecFirst.Core.DecisionTable;

    public interface ITestSourceGenerator
    {
        string[] Generate(string namespaceName, DecisionTable[] decisionTables);
    }
}