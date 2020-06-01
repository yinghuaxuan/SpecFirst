
namespace DecisionMarkd.Template.xUnit
{
    public interface ITemplateGenerator
    {
        string[] Generate(string namespaceName, DecisionTable.DecisionTable[] decisionTables);
    }
}