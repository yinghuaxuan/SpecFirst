namespace SpecFirst.Core.Serialization
{
    using System;
    using System.Diagnostics;

    public class IntegerDataSerializer : IDataSerializer
    {
        public string Serialize(object data)
        {
            Debug.Assert(data is int);

            return data.ToString();
        }
    }
}
