namespace SpecFirst.Core.DecisionTable.Validator
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Linq;

    public sealed class DecisionTableHtmlValidator : IDecisionTableHtmlValidator
    {
        public bool Validate(XElement document, out IEnumerable<string> errors)
        {
            var errorList = new List<string>();

            IEnumerable<XElement> rows = document.Descendants("tr").ToList();
            if (rows.Count() < 3)
            {
                errorList.Add("Decision table must have at least three rows");
            }

            XElement firstRow = rows.First();
            IEnumerable<XElement> columns = firstRow.Descendants("td").Union(firstRow.Descendants("th"));
            if (columns.Count() != 1)
            {
                errorList.Add("The first row of the decision table must have a single column");
            }

            if (columns.ElementAt(0).Value.TrimStart().StartsWith("comment", StringComparison.OrdinalIgnoreCase))
            {
                errorList.Add("The first row is a comment row");
            }

            errors = errorList;
            return !errors.Any();
        }
    }
}