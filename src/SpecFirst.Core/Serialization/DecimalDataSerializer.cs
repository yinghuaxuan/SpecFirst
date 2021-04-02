namespace SpecFirst.Core.Serialization
{
    using System.Diagnostics;

    public class DecimalDataSerializer : IDataSerializer
    {
        public string Serialize(object data)
        {
            Debug.Assert(data is decimal);

            return $"{data}M";
        }
    }
}
