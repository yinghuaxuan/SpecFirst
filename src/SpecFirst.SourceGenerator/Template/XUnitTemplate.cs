namespace SpecFirst.TestsGenerator.xUnit
{
    public class XUnitTemplate
    {
        public const string TEST_TEMPLATE = @"
namespace {{namespace_name}}
{
    using System;
    using System.Collections.Generic;
    using Xunit;
    
    {{#each list_of_fixtures}}
    public partial class {{class_name}}
    {
        [Theory]
        [MemberData(nameof(get_test_data))]
        public void {{class_name}}_tests({{test_parameters}})
        {
            {{#if impl_return_values}}
            {{impl_return_values}} = {{class_name}}_implementation({{impl_arguments}});
            {{else}}
            {{class_name}}_implementation({{impl_arguments}});
            {{/if}}
            {{#each assert_statements}}
            {{{this}}};
            {{/each}}
        }

        public static IEnumerable<object[]> get_test_data()
        {
            var data = new List<object[]>
            {
                {{#each test_data_and_comments}}
                new object[] { {{{this.TestData}}} }, // {{this.Comment}}
                {{/each}}
            };

            return data;
        }

        private partial {{impl_return_types}} {{class_name}}_implementation({{impl_parameters}});
    }

    {{/each}}
}";

        public const string IMPLEMENTATION_TEMPLATE = @"
namespace {{namespace_name}}
{
    using System;

    {{#each list_of_fixtures}}
    public partial class {{class_name}}
    {
        private partial {{impl_return_types}} {{class_name}}_implementation({{impl_parameters}})
        {
            throw new NotImplementedException();
        }
    }

    {{/each}}
}";
    }
}
