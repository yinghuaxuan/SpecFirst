namespace SpecFirst.TestsGenerator.xUnit
{
    using System.Linq;
    using SpecFirst.Core.DecisionTable;
    using SpecFirst.Serialization;
    using SpecFirst.TestsGenerator.Converter;
    using SpecFirst.TestsGenerator.Serialization;

    public class XUnitTemplateDataProvider
    {
        private readonly SnakeCaseNamingStrategy _namingStrategy;
        private readonly StringDataSerializer _stringSerializer;
        private readonly DateTimeDataSerializer _datetimeSerializer;
        private readonly BooleanDataSerializer _booleanSerializer;
        private readonly TableDataToTestDataConverter _tableDataToTestDataConverter;
        private readonly TableHeaderToParametersConverter _tableHeaderToParametersConverter;

        public XUnitTemplateDataProvider()
        {
            _namingStrategy = new SnakeCaseNamingStrategy();
            _stringSerializer = new StringDataSerializer();
            _datetimeSerializer = new DateTimeDataSerializer();
            _booleanSerializer = new BooleanDataSerializer();
            _tableDataToTestDataConverter = new TableDataToTestDataConverter(_stringSerializer, _datetimeSerializer, _booleanSerializer);
            _tableHeaderToParametersConverter = new TableHeaderToParametersConverter();
        }

        public XUnitTemplateData[] GetTemplateData(DecisionTable[] decisionTables)
        {
            XUnitTemplateData[] templateData = new XUnitTemplateData[decisionTables.Length];
            for (int i = 0; i < decisionTables.Length; i++)
            {
                DecisionTable decisionTable = decisionTables[i];
                XUnitTemplateData singleTemplateData = GetSingleTemplateData(decisionTable);
                templateData[i] = singleTemplateData;
            }

            return templateData;
        }

        private XUnitTemplateData GetSingleTemplateData(DecisionTable decisionTable)
        {
            XUnitTemplateData templateData = new XUnitTemplateData();
            templateData.ClassName = _namingStrategy.Parse(decisionTable.TableName);
            string[] methodParameters = 
                _tableHeaderToParametersConverter.Convert(decisionTable.TableHeaders, out int[] selectedDataIndices);
            templateData.TestMethodParameterDeclarations = methodParameters[0];
            templateData.ImplementationMethodParameterDeclarations = methodParameters[1];
            templateData.ImplementationMethodParameters = methodParameters[2];
            templateData.TestData = 
                _tableDataToTestDataConverter.Convert(
                    decisionTable.TableHeaders.Select(v => v.DataType).ToArray(),
                    decisionTable.TableData,
                    selectedDataIndices);
            return templateData;
        }

    }
}
