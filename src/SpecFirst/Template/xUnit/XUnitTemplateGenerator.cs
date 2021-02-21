using HandlebarsDotNet;
using SpecFirst.Template.xUnit;
using System;
using System.Linq;

namespace DecisionMarkd.Template.xUnit
{
    public class XUnitSourceGenerator : ITestSourceGenerator
    {
        private XUnitTemplateDataProvider _templateDataProvider;

        public XUnitSourceGenerator()
        {
            _templateDataProvider = new XUnitTemplateDataProvider();
        }

        public string[] Generate(string namespaceName, SpecFirst.DecisionTable.DecisionTable[] decisionTables)
        {
            XUnitTemplateData[] templateData = _templateDataProvider.GetTemplateData(decisionTables);
            var data = new
            {
                namespace_name = namespaceName,
                list_of_fixtures = templateData.Select(t => new
                {
                    class_name = t.ClassName,
                    parameter_declarations = t.TestMethodParameterDeclarations,
                    parameters = t.ImplementationMethodParameters,
                    list_of_test_data = t.TestData
                })
            };

            string testSources = GenerateTestSources(data);
            string implementationSources = GenerateImplmentationSources(data);

            return new[] { testSources, implementationSources };
        }

        private string GenerateTestSources(object data)
        {
            Func<object, string> compiled = Handlebars.Compile(XUnitTemplate.TEST_TEMPLATE);

            return compiled(data);
        }

        private string GenerateImplmentationSources(object data)
        {
            Func<object, string> compiled = Handlebars.Compile(XUnitTemplate.IMPLEMENTATION_TEMPLATE);

            return compiled(data);
        }
    }
}
