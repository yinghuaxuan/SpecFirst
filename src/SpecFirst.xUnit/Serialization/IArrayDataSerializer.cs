namespace SpecFirst.xUnit.Serialization
{
    using System;

    public interface IArrayDataSerializer
    {
        string Serialize(object data, Type targetType);
    }
}