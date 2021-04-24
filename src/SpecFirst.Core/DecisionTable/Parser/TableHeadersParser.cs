namespace SpecFirst.Core.DecisionTable.Parser
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Linq;

    public sealed class TableHeadersParser
    {
        private readonly TableHeaderParser _tableHeaderParser;

        public TableHeadersParser()
        {
             _tableHeaderParser = new TableHeaderParser();
        }

        public IEnumerable<TableHeader> Parse(XElement decisionTable)
        {
            var rows = decisionTable.Descendants("tr");
            XElement secondRow = rows.Skip(1).First();
            var columns = secondRow.Descendants("td").Union(secondRow.Descendants("th"));
            return columns.Select(c => _tableHeaderParser.Parse(c.Value));
        }
    }
}