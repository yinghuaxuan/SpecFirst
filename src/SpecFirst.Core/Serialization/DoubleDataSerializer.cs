namespace SpecFirst.Core.Serialization
{
    using System.Diagnostics;

    public class DoubleDataSerializer : IDataSerializer
    {
        public string Serialize(object data)
        {
            Debug.Assert(data is double);

            return $"{data}D";
        }
    }
}
