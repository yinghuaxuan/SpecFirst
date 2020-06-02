namespace DecisionMarkd.Serialization
{
    public class BooleanSerializer
    {
        public string Serialize(object data)
        {
            return $"{data.ToString().ToLowerInvariant()}, ";
        }
    }
}
