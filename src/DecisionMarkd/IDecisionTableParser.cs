namespace DecisionMarkd
{
    using System.Xml.Linq;

    public interface IDecisionTableParser
    {
        DecisionTable Parse(XElement element);
    }
}