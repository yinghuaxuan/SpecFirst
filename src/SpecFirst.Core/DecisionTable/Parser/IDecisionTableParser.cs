namespace SpecFirst.Core.DecisionTable.Parser
{
    using System.Xml.Linq;

    public interface IDecisionTableParser
    {
        DecisionTable Parse(XElement element);
    }
}