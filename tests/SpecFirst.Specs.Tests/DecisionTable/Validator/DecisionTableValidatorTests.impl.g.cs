
namespace SpecFirst.Specs.Tests
{
    using System;
    using System.Xml.Linq;
    using SpecFirst.Core.DecisionTable.Validator;

    public partial class decision_table_validator
    {
        private partial (bool, string) decision_table_validator_implementation(string decision_table)
        {
            var table = XElement.Parse(decision_table);
            var valid = new DecisionTableHtmlValidator().Validate(table, out var errors);
            return (valid, string.Join("", errors));
        }
    }

}