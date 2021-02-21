namespace SpecFirst.Template.xUnit
{
    public interface ITestSourceGenerator
    {
        string[] Generate(string namespaceName, DecisionTable.DecisionTable[] decisionTables);
    }
}