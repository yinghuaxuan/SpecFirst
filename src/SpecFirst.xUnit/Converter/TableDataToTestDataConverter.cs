namespace SpecFirst.xUnit.Converter
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using SpecFirst.Core.DecisionTable;
    using SpecFirst.Core.TypeResolver;
    using SpecFirst.xUnit.Serialization;

    public class TableDataToTestDataConverter
    {
        private readonly IPrimitiveDataSerializer _stringSerializer;
        private readonly IPrimitiveDataSerializer _numberSerializer;
        private readonly IPrimitiveDataSerializer _datetimeSerializer;
        private readonly IPrimitiveDataSerializer _booleanSerializer;
        private readonly IArrayDataSerializer _arraySerializer;

        public TableDataToTestDataConverter(
            IPrimitiveDataSerializer stringSerializer,
            IPrimitiveDataSerializer numberSerializer,
            IPrimitiveDataSerializer datetimeSerializer,
            IPrimitiveDataSerializer booleanSerializer,
            IArrayDataSerializer arraySerializer)
        {
            _stringSerializer = stringSerializer;
            _numberSerializer = numberSerializer;
            _datetimeSerializer = datetimeSerializer;
            _booleanSerializer = booleanSerializer;
            _arraySerializer = arraySerializer;
        }

        public string[] Convert(TableHeader[] tableHeaders, object[,] decisionData)
        {
            List<string> testData = new List<string>();
            StringBuilder builder = new StringBuilder();

            for (int i = 0; i < decisionData.GetLength(0); i++)
            {
                builder.Clear();
                for (int j = 0; j < decisionData.GetLength(1); j++)
                {
                    if(tableHeaders[j].TableHeaderType != TableHeaderType.Comment)
                    {
                        var data = Convert(decisionData, i, j, tableHeaders[j].DataType);
                        builder.Append($"{data}, ");
                    }
                }

                testData.Add(builder.Remove(builder.Length - 2, 2).ToString());
            }

            return testData.ToArray();
        }

        private string Convert(object[,] decisionData, int i, int j, Type dataType)
        {
            string data;
            if (decisionData[i, j].GetType().IsArray)
            {
                data = _arraySerializer.Serialize(decisionData[i, j], dataType);
            }
            else
            {
                data = ConvertPrimitiveData(decisionData, i, j);
            }

            return data;
        }

        private string ConvertPrimitiveData(object[,] decisionData, int i, int j)
        {
            string data;
            switch (decisionData[i, j])
            {
                case IntType _:
                case DoubleType _:
                case DecimalType _:
                    data = _numberSerializer.Serialize(decisionData[i, j]);
                    break;
                case DateTime _:
                    data = _datetimeSerializer.Serialize(decisionData[i, j]);
                    break;
                case bool _:
                    data = _booleanSerializer.Serialize(decisionData[i, j]);
                    break;
                case string _:
                    data = _stringSerializer.Serialize(decisionData[i, j]);
                    break;
                default:
                    throw new InvalidOperationException();
            }

            return data;
        }
    }
}
