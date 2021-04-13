namespace SpecFirst.TestsGenerator.xUnit.Converter
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using SpecFirst.Core;
    using SpecFirst.Core.DecisionTable;
    using SpecFirst.Core.Serialization;
    using SpecFirst.Core.TypeResolver;

    public class TableDataToTestDataConverter
    {
        private readonly IDataSerializer _stringSerializer;
        private readonly IDataSerializer _numberSerializer;
        private readonly IDataSerializer _datetimeSerializer;
        private readonly IDataSerializer _booleanSerializer;

        public TableDataToTestDataConverter(
            IDataSerializer stringSerializer,
            IDataSerializer numberSerializer,
            IDataSerializer datetimeSerializer,
            IDataSerializer booleanSerializer)
        {
            _stringSerializer = stringSerializer;
            _numberSerializer = numberSerializer;
            _datetimeSerializer = datetimeSerializer;
            _booleanSerializer = booleanSerializer;
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
                        var data = Convert(decisionData, i, j);
                        builder.Append($"{data}, ");
                    }
                }

                testData.Add(builder.Remove(builder.Length - 2, 2).ToString());
            }

            return testData.ToArray();
        }

        private string Convert(object[,] decisionData, int i, int j)
        {
            string data;
            switch (decisionData[i, j])
            {
                case NumberValue _:
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
