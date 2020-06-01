namespace DecisionMarkd.Template.Serialization
{
    public interface INamingStrategy
    {
        string Parse(string raw);
    }
}