
namespace SpecFirst.Specs.Tests
{
    using System;
    using System.Xml.Linq;
    using SpecFirst.Core.DecisionTable.Validator;

    public partial class is_table_a_decision_table
    {
        private partial (bool, string) is_table_a_decision_table_implementation(string decision_table)
        {
            var table = XElement.Parse(decision_table);
            var valid = new DecisionTableHtmlValidator().Validate(table, out var errors);
            return (valid, string.Join("", errors));
        }
    }

}