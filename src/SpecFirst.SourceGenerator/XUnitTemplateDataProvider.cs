namespace SpecFirst.TestsGenerator.xUnit
{
    using System.Collections.Generic;
    using System.Linq;
    using SpecFirst.Core.Converter;
    using SpecFirst.Core.DecisionTable;
    using SpecFirst.Core.Serialization;

    public class XUnitTemplateDataProvider
    {
        private readonly SnakeCaseNamingStrategy _namingStrategy;
        private readonly TableDataToTestDataConverter _tableDataToTestDataConverter;
        private readonly TableHeaderToTestSignatureConverter _tableHeaderToTestSignatureConverter;
        private readonly TableDataToCommentsConverter _tableDataToCommentsConverter;

        public XUnitTemplateDataProvider()
        {
            var stringSerializer = new StringDataSerializer();
            var datetimeSerializer = new DateTimeDataSerializer();
            var booleanSerializer = new BooleanDataSerializer();
            var numberSerializer = new NumberDataSerializer();
            _tableDataToTestDataConverter = new TableDataToTestDataConverter(stringSerializer, numberSerializer, datetimeSerializer, booleanSerializer);
            _namingStrategy = new SnakeCaseNamingStrategy();
            var parameterConverter = new TableHeaderToParametersConverter(_namingStrategy);
            _tableHeaderToTestSignatureConverter = new TableHeaderToTestSignatureConverter(parameterConverter);
            _tableDataToCommentsConverter = new TableDataToCommentsConverter();
        }

        public XUnitTemplateData[] GetTemplateData(DecisionTable[] decisionTables)
        {
            XUnitTemplateData[] templateData = new XUnitTemplateData[decisionTables.Length];
            for (int i = 0; i < decisionTables.Length; i++)
            {
                DecisionTable decisionTable = decisionTables[i];
                XUnitTemplateData singleTemplateData = GetTemplateData(decisionTable);
                templateData[i] = singleTemplateData;
            }

            return templateData;
        }

        private XUnitTemplateData GetTemplateData(DecisionTable decisionTable)
        {
            var methodParameters = 
                _tableHeaderToTestSignatureConverter.Convert(decisionTable.TableHeaders);
            var testData = 
                _tableDataToTestDataConverter.Convert(decisionTable.TableHeaders, decisionTable.TableData);
            var comments =
                _tableDataToCommentsConverter.Convert(decisionTable.TableHeaders.ToArray(), decisionTable.TableData);
            XUnitTemplateData templateData = new XUnitTemplateData
            {
                ClassName = _namingStrategy.Resolve(decisionTable.TableName),
                TestMethodParameters = methodParameters[0],
                ImplMethodParameters = methodParameters[1],
                ImplMethodArguments = methodParameters[2],
                ImplMethodReturnTypes = methodParameters[3],
                ImplMethodReturnValues = methodParameters[4],
                AssertStatements = methodParameters[5],
                TestDataAndComments = GetTestDataAndComments(testData, comments)
            };

            return templateData;
        }

        private List<TestDataAndComment> GetTestDataAndComments(string[] testData, string[] comments)
        {
            var testDataAndComments = new List<TestDataAndComment>();
            for (int i = 0; i < testData.Length; i++)
            {
                testDataAndComments.Add(new TestDataAndComment{TestData = testData[i], Comment = comments[i]});
            }

            return testDataAndComments;
        }
    }
}
