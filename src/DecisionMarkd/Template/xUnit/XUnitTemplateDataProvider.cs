using DecisionMarkd.Converter;
using DecisionMarkd.Serialization;
using DecisionMarkd.Template.Serialization;
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
            string[] methodParameters = 
                _decisionVariablesToParametersConverter.Convert(decisionTable.Variables, out int[] selectedDataIndices);
            templateData.TestMethodParameterDeclarations = methodParameters[0];
            templateData.ImplementationMethodParameterDeclarations = methodParameters[1];
            templateData.ImplementationMethodParameters = methodParameters[2];
            templateData.TestData = 
                _decisionDataToTestDataConverter.Convert(
                    decisionTable.Variables.Select(v => v.DataType).ToArray(),
                    decisionTable.DecisionData,
                    selectedDataIndices);
            return templateData;
        }

    }
}
