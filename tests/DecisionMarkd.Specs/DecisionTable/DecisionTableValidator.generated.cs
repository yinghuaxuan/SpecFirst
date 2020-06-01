
namespace DecisionMarkd.Tests
{
    using System.Collections.Generic;
    using Xunit;

    public partial class decision_table_validator
    {
        [Theory]
        [MemberData(nameof(get_test_data))]
        public void decision_table_validator_tests(object comment, object decision_table, object is_valid, object validation_error)
        {
            decision_table_validator_implementation(comment, decision_table, is_valid, validation_error);
        }

        public static IEnumerable<object[]> get_test_data()
        {
            var data = new List<object[]>
            {
                new object[] {
                    "<Table>  with only 1 name row",
                    -1,
                    false,
                    "Decision table must have at least three rows" }
            };

            return data;
        }

        partial void decision_table_validator_implementation(object comment, object decision_table, object is_valid, object validation_error);
    }
}
