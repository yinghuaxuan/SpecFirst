namespace MarkedDecision
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Linq;

    public sealed class DecisionTableValidator : IDecisionTableValidator
    {
        public ValidationResult Validate(XElement document)
        {
            IList<string> errors = new List<string>();

            IEnumerable<XElement> rows = document.Descendants("tr");
            if (rows.Count() < 3)
            {
                errors.Add("decision table must have at least three rows");
            }

            XElement firstRow = rows.First();
            IEnumerable<XElement> columns = firstRow.Descendants("td");
            if (columns.Count() != 1)
            {
                errors.Add("The first row of the decision table must have a single column");
            }

            // Validate the headers, e.g. any invalid characters for class names

            return new ValidationResult(errors);
        }
    }
}