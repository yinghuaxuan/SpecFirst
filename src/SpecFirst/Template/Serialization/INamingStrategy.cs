namespace SpecFirst.Template.Serialization
{
    public interface INamingStrategy
    {
        string Parse(string raw);
    }
}