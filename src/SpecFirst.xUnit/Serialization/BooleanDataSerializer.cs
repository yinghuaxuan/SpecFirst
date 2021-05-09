namespace SpecFirst.xUnit.Serialization
{
    using System.Diagnostics;

    public class BooleanDataSerializer : IPrimitiveDataSerializer
    {
        public string Serialize(object data)
        {
            Debug.Assert(data is bool);

            return data.ToString().ToLowerInvariant();
        }
    }
}
