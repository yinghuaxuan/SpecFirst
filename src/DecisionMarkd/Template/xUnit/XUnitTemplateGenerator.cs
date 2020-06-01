using HandlebarsDotNet;
using System;
using System.Text;

namespace DecisionMarkd.Template.xUnit
{
    public class XUnitTemplateGenerator : ITemplateGenerator
    {
        private XUnitTemplateDataProvider _templateDataProvider;

        public XUnitTemplateGenerator()
        {
            _templateDataProvider = new XUnitTemplateDataProvider();
        }

        public string[] Generate(string namespaceName, DecisionTable.DecisionTable[] decisionTables)
        {
            string testSources = GenerateTestSources(namespaceName, decisionTables);
            string implementationSources = GenerateImplmentationSources(namespaceName, decisionTables);

            return new[] { testSources, implementationSources };
        }

        private string GenerateTestSources(string namespaceName, DecisionTable.DecisionTable[] decisionTables)
        {
            Func<object, string> compiled = Handlebars.Compile(XUnitTemplate.TEST_TEMPLATE);
            StringBuilder testSources = new StringBuilder();
            foreach (DecisionTable.DecisionTable table in decisionTables)
            {
                XUnitTemplateData templateData = _templateDataProvider.GetTemplateData(table);
                string testSource = compiled(new
                {
                    namespace_name = namespaceName,
                    class_name = templateData.ClassName,
                    parameter_declarations = templateData.TestMethodParameterDeclarations,
                    parameters = templateData.ImplementationMethodParameters,
                    list_of_test_data = templateData.TestData,
                });

                testSources.AppendLine(testSource);
            }

            return testSources.ToString();
        }

        private string GenerateImplmentationSources(string namespaceName, DecisionTable.DecisionTable[] decisionTables)
        {
            Func<object, string> compiled = Handlebars.Compile(XUnitTemplate.IMPLEMENTATION_TEMPLATE);
            StringBuilder testSources = new StringBuilder();
            foreach (DecisionTable.DecisionTable table in decisionTables)
            {
                XUnitTemplateData templateData = _templateDataProvider.GetTemplateData(table);
                string testSource = compiled(new
                {
                    namespace_name = namespaceName,
                    class_name = templateData.ClassName,
                    parameter_declarations = templateData.TestMethodParameterDeclarations,
                });

                testSources.AppendLine(testSource);
            }

            return testSources.ToString();
        }
    }
}
