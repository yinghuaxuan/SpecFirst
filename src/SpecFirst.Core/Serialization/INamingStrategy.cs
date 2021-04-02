namespace SpecFirst.Core.Serialization
{
    public interface INamingStrategy
    {
        string Resolve(string raw);
    }
}