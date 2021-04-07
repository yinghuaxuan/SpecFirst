﻿namespace SpecFirst.Core.DecisionTable.Validator
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Linq;

    public sealed class DecisionTableHtmlValidator : IDecisionTableHtmlValidator
    {
        public bool Validate(XElement document, out IEnumerable<string> errors)
        {
            errors = new List<string>();

            IEnumerable<XElement> rows = document.Descendants("tr").ToList();
            if (rows.Count() < 3)
            {
                errors.Append("Decision table must have at least three rows");
            }

            XElement firstRow = rows.First();
            IEnumerable<XElement> columns = firstRow.Descendants("td").Union(firstRow.Descendants("th"));
            if (columns.Count() != 1)
            {
                errors.Append("The first row of the decision table must have a single td or th column");
            }

            return !errors.Any();
        }
    }
}