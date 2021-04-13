namespace SpecFirst.Core.Serialization
{
    using System.Diagnostics;

    public class BooleanDataSerializer : IDataSerializer
    {
        public string Serialize(object data)
        {
            Debug.Assert(data is bool);

            return data.ToString().ToLowerInvariant();
        }
    }
}
