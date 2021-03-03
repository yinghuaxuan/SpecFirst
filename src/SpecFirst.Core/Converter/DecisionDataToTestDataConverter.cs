namespace SpecFirst.Core.Converter
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using SpecFirst.Serialization;

    public class DecisionDataToTestDataConverter
    {
        private readonly StringDataSerializer _stringSerializer;
        private readonly DateTimeDataSerializer _datetimeSerializer;
        private readonly BooleanDataSerializer _booleanSerializer;

        public DecisionDataToTestDataConverter(StringDataSerializer stringSerializer, DateTimeDataSerializer datetimeSerializer, BooleanDataSerializer booleanSerializer)
        {
            _stringSerializer = stringSerializer;
            _datetimeSerializer = datetimeSerializer;
            _booleanSerializer = booleanSerializer;
        }

        public string[] Convert(Type[] dataTypes, object[,] decisionData, int[] selectedDataIndices)
        {
            List<string> testData = new List<string>();
            StringBuilder builder = new StringBuilder();

            for (int i = 0; i < decisionData.GetLength(0); i++)
            {
                builder.Clear();
                for (int j = 0; j < selectedDataIndices.Length; j++)
                {
                    string data;
                    int index = selectedDataIndices[j];
                    if (dataTypes[index] == typeof(string))
                    {
                        data = _stringSerializer.Serialize(decisionData[i, index]);
                    }
                    else if (dataTypes[index] == typeof(DateTime) || dataTypes[index] == typeof(DateTime?))
                    {
                        data = _datetimeSerializer.Serialize(decisionData[i, index]);
                    }
                    else if (dataTypes[index] == typeof(bool) || dataTypes[index] == typeof(bool?))
                    {
                        data = _booleanSerializer.Serialize(decisionData[i, index]);
                    }
                    else
                    {
                        data = $"{decisionData[i, index]}, ";
                    }

                    builder.Append(data);
                }

                testData.Add(builder.Remove(builder.Length - 2, 2).ToString());
            }

            return testData.ToArray();
        }
    }
}
