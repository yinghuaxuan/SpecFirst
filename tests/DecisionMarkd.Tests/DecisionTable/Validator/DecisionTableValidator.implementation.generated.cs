
namespace DecisionMarkd.Tests
{
    using DecisionMarkd.DecisionTable.Validator;
    using System;
    using System.IO;
    using System.Linq;
    using System.Xml.Linq;
    using Xunit;

    public partial class decision_table_validator
    {
        partial void decision_table_validator_implementation(String decision_table, Boolean is_valid, String validation_error)
        {
            XDocument document = XDocument.Parse("<html>" + decision_table + "</html>", LoadOptions.None);
            XElement table = document.Descendants("table").First();
            ValidationResult validationResult = new DecisionTableHtmlValidator().Validate(table);
            Assert.Equal(validationResult.IsValid, is_valid);
            Assert.Equal(validationResult.Error, validation_error);
        }
    }

}