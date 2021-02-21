namespace SpecFirst.Template.Serialization
{
    public class SnakeCaseNamingStrategy : INamingStrategy
    {
        public string Parse(string raw)
        {
            return raw.ToLowerInvariant().Replace(" ", "_");
        }
    }
}
