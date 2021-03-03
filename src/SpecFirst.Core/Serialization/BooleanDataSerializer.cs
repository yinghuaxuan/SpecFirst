namespace SpecFirst.Serialization
{
    public class BooleanDataSerializer
    {
        public string Serialize(object data)
        {
            return $"{data.ToString().ToLowerInvariant()}, ";
        }
    }
}
