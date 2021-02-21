using DecisionMarkd.Converter;
using SpecFirst.Serialization;
using SpecFirst.Template.Serialization;
using System.Linq;

namespace DecisionMarkd.Template.xUnit
{
    public class XUnitTemplateDataProvider
    {
        private SnakeCaseNamingStrategy _namingStrategy;
        private StringDataSerializer _stringSerializer;
        private DateTimeDataSerializer _datetimeSerializer;
        private BooleanDataSerializer _booleanSerializer;
        private DecisionDataToTestDataConverter _decisionDataToTestDataConverter;
        private DecisionVariablesToParametersConverter _decisionVariablesToParametersConverter;

        public XUnitTemplateDataProvider()
        {
            _namingStrategy = new SnakeCaseNamingStrategy();
            _stringSerializer = new StringDataSerializer();
            _datetimeSerializer = new DateTimeDataSerializer();
            _booleanSerializer = new BooleanDataSerializer();
            _decisionDataToTestDataConverter = new DecisionDataToTestDataConverter(_stringSerializer, _datetimeSerializer, _booleanSerializer);
            _decisionVariablesToParametersConverter = new DecisionVariablesToParametersConverter();
        }

        public XUnitTemplateData[] GetTemplateData(SpecFirst.DecisionTable.DecisionTable[] decisionTables)
        {
            XUnitTemplateData[] templateData = new XUnitTemplateData[decisionTables.Length];
            for (int i = 0; i < decisionTables.Length; i++)
            {
                SpecFirst.DecisionTable.DecisionTable decisionTable = decisionTables[i];
                XUnitTemplateData singleTemplateData = GetSingleTemplateData(decisionTable);
                templateData[i] = singleTemplateData;
            }

            return templateData;
        }

        private XUnitTemplateData GetSingleTemplateData(SpecFirst.DecisionTable.DecisionTable decisionTable)
        {
            XUnitTemplateData templateData = new XUnitTemplateData();
            templateData.ClassName = _namingStrategy.Parse(decisionTable.TableName);
            string[] methodParameters = 
                _decisionVariablesToParametersConverter.Convert(decisionTable.TableHeaders, out int[] selectedDataIndices);
            templateData.TestMethodParameterDeclarations = methodParameters[0];
            templateData.ImplementationMethodParameterDeclarations = methodParameters[1];
            templateData.ImplementationMethodParameters = methodParameters[2];
            templateData.TestData = 
                _decisionDataToTestDataConverter.Convert(
                    decisionTable.TableHeaders.Select(v => v.DataType).ToArray(),
                    decisionTable.TableData,
                    selectedDataIndices);
            return templateData;
        }

    }
}
