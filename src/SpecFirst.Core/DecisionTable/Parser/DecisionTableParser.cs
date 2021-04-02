namespace SpecFirst.Core.DecisionTable.Parser
{
    using System;
    using System.Linq;
    using System.Xml.Linq;

    public sealed class DecisionTableParser : IDecisionTableParser
    {
        private readonly TableNameParser _tableNameParser;
        private readonly TableHeadersParser _tableHeadersParser;
        private readonly TableDataParser _tableDataParser;

        public DecisionTableParser()
        {
            _tableNameParser = new TableNameParser();
            _tableHeadersParser = new TableHeadersParser();
            _tableDataParser = new TableDataParser();
        }

        public DecisionTable Parse(XElement table)
        {
            var tableName = _tableNameParser.Parse(table);
            var tableHeaders = _tableHeadersParser.Parse(table).ToArray();
            object[,] tableData = _tableDataParser.Parse(table, out Type[] dataTypes);
            UpdateTableHeaderTypesBasedOnData(tableHeaders, dataTypes);
            return new DecisionTable(tableName, tableHeaders, tableData);
        }

        private static void UpdateTableHeaderTypesBasedOnData(TableHeader[] variables, Type[] types)
        {
            for (int i = 0; i < variables.Count(); i++)
            {
                variables[i].UpdateDataType(types[i]);
            }
        }
    }
}