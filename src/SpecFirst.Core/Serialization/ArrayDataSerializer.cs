namespace SpecFirst.Core.Serialization
{
    using System;
    using System.Diagnostics;
    using System.Linq;
    using SpecFirst.Core.TypeResolver;

    public class ArrayDataSerializer : IArrayDataSerializer
    {
        private readonly IPrimitiveDataSerializer _stringSerializer;
        private readonly IPrimitiveDataSerializer _numberSerializer;
        private readonly IPrimitiveDataSerializer _datetimeSerializer;
        private readonly IPrimitiveDataSerializer _booleanSerializer;

        public ArrayDataSerializer(
            IPrimitiveDataSerializer stringSerializer,
            IPrimitiveDataSerializer numberSerializer,
            IPrimitiveDataSerializer datetimeSerializer,
            IPrimitiveDataSerializer booleanSerializer)
        {
            _stringSerializer = stringSerializer;
            _numberSerializer = numberSerializer;
            _datetimeSerializer = datetimeSerializer;
            _booleanSerializer = booleanSerializer;
        }

        public string Serialize(object data, Type targetType)
        {
            Debug.Assert(data.GetType().IsArray);

            return SerializeArray(data, targetType);
        }

        private string SerializeArray(object data, Type targetType)
        {
            string serialized;
            if (targetType == typeof(IntType[]))
            {
                serialized = $"new int[] {{{string.Join(", ", (data as object[]).Select(SerializeObject))}}}";
            }
            else if (targetType == typeof(DecimalType[]))
            {
                serialized = $"new decimal[] {{{string.Join(", ", (data as object[]).Select(SerializeObject))}}}";
            }
            else if (targetType == typeof(DoubleType[]))
            {
                serialized = $"new double[] {{{string.Join(", ", (data as object[]).Select(SerializeObject))}}}";
            }
            else if (targetType == typeof(bool[]))
            {
                serialized = $"new bool[] {{{string.Join(", ", (data as object[]).Select(SerializeObject))}}}";
            }
            else if (targetType == typeof(DateTime[]))
            {
                serialized = $"new DateTime[] {{{string.Join(", ", (data as object[]).Select(SerializeObject))}}}";
            }
            else if (targetType == typeof(string[]))
            {
                serialized = $"new string[] {{{string.Join(", ", (data as object[]).Select(SerializeObject))}}}";
            }
            else if (targetType == typeof(object[]))
            {
                serialized = $"new object[] {{{string.Join(", ", (data as object[]).Select(SerializeObject))}}}";
            }
            else
            {
                throw new InvalidOperationException();
            }

            return serialized;
        }

        private string SerializeObject(object value)
        {
            string data;
            switch (value)
            {
                case IntType _:
                case DoubleType _:
                case DecimalType _:
                    data = _numberSerializer.Serialize(value);
                    break;
                case DateTime _:
                    data = _datetimeSerializer.Serialize(value);
                    break;
                case bool _:
                    data = _booleanSerializer.Serialize(value);
                    break;
                case string _:
                    data = _stringSerializer.Serialize(value);
                    break;
                default:
                    throw new InvalidOperationException();
            }

            return data;
        }
    }
}
