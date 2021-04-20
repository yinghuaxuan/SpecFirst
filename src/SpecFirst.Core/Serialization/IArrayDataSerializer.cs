namespace SpecFirst.Core.Serialization
{
    using System;

    public interface IArrayDataSerializer
    {
        string Serialize(object data, Type targetType);
    }
}