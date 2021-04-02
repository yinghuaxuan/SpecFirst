namespace SpecFirst.TestsGenerator.xUnit
{
    using System;
    using System.Linq;
    using HandlebarsDotNet;
    using SpecFirst.Core;
    using SpecFirst.Core.DecisionTable;

    public class XUnitTestsGenerator : ITestsGenerator
    {
        private readonly XUnitTemplateDataProvider _templateDataProvider;

        public XUnitTestsGenerator()
        {
            _templateDataProvider = new XUnitTemplateDataProvider();
        }

        public string[] Generate(string namespaceName, DecisionTable[] decisionTables)
        {
            XUnitTemplateData[] templateData = _templateDataProvider.GetTemplateData(decisionTables);
            var data = new
            {
                namespace_name = namespaceName,
                list_of_fixtures = templateData.Select(t => new
                {
                    class_name = t.ClassName,
                    test_parameters = t.TestMethodParameters,
                    impl_arguments = t.ImplMethodArguments,
                    impl_parameters = t.ImplMethodParameters,
                    impl_return_values = t.ImplMethodReturnValues,
                    list_of_test_data = t.TestData
                })
            };

            string testSources = GenerateTestMethods(data);
            string implementationSources = GenerateTestImplementations(data);

            return new[] { testSources, implementationSources };
        }

        private string GenerateTestMethods(object data)
        {
            Func<object, string> compiled = Handlebars.Compile(XUnitTemplate.TEST_TEMPLATE);

            return compiled(data);
        }

        private string GenerateTestImplementations(object data)
        {
            Func<object, string> compiled = Handlebars.Compile(XUnitTemplate.IMPLEMENTATION_TEMPLATE);

            return compiled(data);
        }
    }
}
