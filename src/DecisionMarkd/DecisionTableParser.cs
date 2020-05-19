namespace DecisionMarkd
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Linq;

    public sealed class DecisionTableParser : IDecisionTableParser
    {
        public DecisionTable Parse(XElement element)
        {
            IEnumerable<XElement> rows = element.Descendants("tr");
            if (rows.Count() < 3)
            {
                throw new InvalidOperationException($"A DecisionTable must have 3 rows. The table currently only have {rows.Count()} rows");
            }

            IEnumerable<XElement> firstRowColumns = rows.First().Descendants("td");
            if (firstRowColumns.Count() != 1)
            {
                throw new InvalidOperationException($"A first row of the DecisionTable must have only 1 columns. The table currently only have {rows.Count()} rows");
            }
            string className = firstRowColumns.First().Value.ToLowerInvariant().Replace(" ", "_");

            IEnumerable<XElement> secondRowColumns = rows.Skip(1).First().Descendants("td");
            string parameters = string.Join(", ", secondRowColumns.Select(e => $"object {e.Value.ToLowerInvariant().Replace(" ", "_").Replace("?", "")}"));

            List<string> testData = new List<string>();
            foreach (XElement row in rows.Skip(2))
            {
                IEnumerable<XElement> columns = row.Descendants("td");
                string data = string.Join(",", columns.Select(e => e.Value));
                testData.Add(data);
            }

            return new DecisionTable(className, parameters, testData);
        }
    }
}