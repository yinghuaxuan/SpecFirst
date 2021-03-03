namespace SpecFirst.Core.DecisionTable.Validator
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Linq;

    public sealed class DecisionTableHtmlValidator : IDecisionTableHtmlValidator
    {
        public ValidationResult Validate(XElement document)
        {
            string error = null;

            IEnumerable<XElement> rows = document.Descendants("tr");
            if (rows.Count() < 3)
            {
                return new ValidationResult("Decision table must have at least three rows");
            }

            XElement firstRow = rows.First();
            IEnumerable<XElement> columns = firstRow.Descendants("td");
            if (columns.Count() != 1)
            {
                return new ValidationResult("The first row of the decision table must have a single column");
            }

            return new ValidationResult(error);
        }
    }
}