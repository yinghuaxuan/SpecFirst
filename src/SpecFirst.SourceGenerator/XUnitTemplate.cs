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
            {{class_name}}_implementation({{impl_arguments}});
        }

        public static IEnumerable<object[]> get_test_data()
        {
            var data = new List<object[]>
            {
                {{#each list_of_test_data}}
                new object[] { {{{this}}} },
                {{/each}}
            };

            return data;
        }

        private partial {{impl_return_values}} {{class_name}}_implementation({{impl_parameters}});
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
        private partial {{impl_return_values}} {{class_name}}_implementation({{impl_parameters}})
        {
            throw new NotImplementedException();
        }
    }

    {{/each}}
}";
    }
}
