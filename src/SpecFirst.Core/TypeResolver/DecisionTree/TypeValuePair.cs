namespace SpecFirst.Core.TypeResolver
{
    using System;

    public class TypeValuePair
    {
        public Type Type { get; }
        public object Value { get; }

        public TypeValuePair(Type type, object value)
        {
            Type = type;
            Value = value;
        }
    }
}