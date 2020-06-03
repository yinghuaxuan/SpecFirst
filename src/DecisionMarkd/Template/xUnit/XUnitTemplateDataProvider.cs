using DecisionMarkd.DecisionTable;
using DecisionMarkd.Serialization;
using DecisionMarkd.Template.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace DecisionMarkd.Template.xUnit
{
    public class XUnitTemplateDataProvider
    {
        private SnakeCaseNamingStrategy _namingStrategy;
        private StringDataSerializer _stringSerializer;
        private DateTimeDataSerializer _datetimeSerializer;
        private BooleanDataSerializer _booleanSerializer;

        public XUnitTemplateDataProvider()
        {
            _namingStrategy = new SnakeCaseNamingStrategy();
            _stringSerializer = new StringDataSerializer();
            _datetimeSerializer = new DateTimeDataSerializer();
            _booleanSerializer = new BooleanDataSerializer();
        }

        public XUnitTemplateData[] GetTemplateData(DecisionTable.DecisionTable[] decisionTables)
        {
            XUnitTemplateData[] templateData = new XUnitTemplateData[decisionTables.Length];
            for (int i = 0; i < decisionTables.Length; i++)
            {
                DecisionTable.DecisionTable decisionTable = decisionTables[i];
                XUnitTemplateData singleTemplateData = GetSingleTemplateData(decisionTable);
                templateData[i] = singleTemplateData;
            }

            return templateData;
        }

        private XUnitTemplateData GetSingleTemplateData(DecisionTable.DecisionTable decisionTable)
        {
            XUnitTemplateData templateData = new XUnitTemplateData();
            templateData.ClassName = _namingStrategy.Parse(decisionTable.FixtureName);
            templateData.TestMethodParameterDeclarations = GetTestMethodParameters(decisionTable.Variables, out int[] dataIndices);
            string[] implementationMethodParameters = GetImplementationMethodParameters(decisionTable.Variables);
            templateData.ImplementationMethodParameterDeclarations = implementationMethodParameters[0];
            templateData.ImplementationMethodParameters = implementationMethodParameters[1];
            templateData.TestData = GetTestData(decisionTable.Variables, decisionTable.DecisionData, dataIndices);
            return templateData;
        }

        private string[] GetTestData(DecisionVariable[] variables, object[,] decisionData, int[] dataIndices)
        {
            List<string> testData = new List<string>();
            StringBuilder builder = new StringBuilder();

            for (int i = 0; i < decisionData.GetLength(0); i++)
            {
                builder.Clear();
                for (int j = 0; j < dataIndices.Length; j++)
                {
                    string data;
                    int index = dataIndices[j];
                    if (variables[index].DataType == typeof(string))
                    {
                        data = _stringSerializer.Serialize(decisionData[i, index]);
                    }
                    else if (variables[index].DataType == typeof(DateTime) || variables[index].DataType == typeof(DateTime?))
                    {
                        data = _datetimeSerializer.Serialize(decisionData[i, index]);
                    }
                    else if (variables[index].DataType == typeof(bool) || variables[index].DataType == typeof(bool?))
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

        private string GetTestMethodParameters(DecisionVariable[] variables, out int[] indices)
        {
            List<int> inputIndices = new List<int>();
            StringBuilder parameters = new StringBuilder();

            for (int i = 0; i < variables.Length; i++)
            {
                if (variables[i].VariableType != DecisionVariableType.Comment)
                {
                    inputIndices.Add(i);
                    parameters.Append($"{variables[i].DataType.Name} {_namingStrategy.Parse(variables[i].Name)}, ");
                }
            }

            indices = inputIndices.ToArray();
            return parameters.Remove(parameters.Length - 2, 2).ToString();
        }

        private string[] GetImplementationMethodParameters(DecisionVariable[] variables)
        {
            StringBuilder parameters = new StringBuilder();
            StringBuilder parameterWithoutTypes = new StringBuilder();

            for (int i = 0; i < variables.Length; i++)
            {
                if (variables[i].VariableType != DecisionVariableType.Comment)
                {
                    parameters.Append($"{variables[i].DataType.Name} {_namingStrategy.Parse(variables[i].Name)}, ");
                    parameterWithoutTypes.Append($"{_namingStrategy.Parse(variables[i].Name)}, ");
                }
            }

            return new[]
            {
                parameters.Remove(parameters.Length - 2, 2).ToString(),
                parameterWithoutTypes.Remove(parameterWithoutTypes.Length - 2, 2).ToString() 
            };
        }
    }
}
