using SpecFirst.DecisionTable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace SpecFirst.DecisionTable.Parser
{
    public sealed class DecisionTableParser : IDecisionTableParser
    {
        private TableNameParser _tableNameParser;
        private TableHeaderParser _tableHeaderParser;
        private TableDataParser _tableDataParser;

        public DecisionTableParser()
        {
            _tableNameParser = new TableNameParser();
            _tableHeaderParser = new TableHeaderParser();
            _tableDataParser = new TableDataParser();
        }

        public DecisionTable Parse(XElement table)
        {
            IEnumerable<XElement> rows = table.Descendants("tr");
            if (rows.Count() < 3)
            {
                throw new InvalidOperationException($"A DecisionTable must have 3 rows. The table currently only have {rows.Count()} rows");
            }

            string tableName = TryGetTableName(rows.First());
            string className = _tableNameParser.Parse(tableName);

            IEnumerable<XElement> headerRowColumns = rows.Skip(1).First().Descendants("td");
            TableHeader[] variables = headerRowColumns.Select(e => _tableHeaderParser.Parse(e.Value)).ToArray();

            IEnumerable<XElement> dataRows = rows.Skip(2);
            string[,] data = GetDecisionData(headerRowColumns.Count(), dataRows);

            object[,] decisionData = _tableDataParser.Parse(data, out Type[] dataTypes);

            UpdateTableHeaderTypesBasedOnData(variables, dataTypes);

            return new DecisionTable(className, variables, decisionData);
        }

        private static void UpdateTableHeaderTypesBasedOnData(TableHeader[] variables, Type[] types)
        {
            for (int i = 0; i < variables.Count(); i++)
            {
                variables[i].UpdateDataType(types[i]);
            }
        }

        private static string[,] GetDecisionData(int numberOfColumns, IEnumerable<XElement> dataRows)
        {
            string[,] data = new string[dataRows.Count(), numberOfColumns];
            for (int i = 0; i < dataRows.Count(); i++)
            {
                IEnumerable<XElement> columns = dataRows.ElementAt(i).Descendants("td");
                for (int j = 0; j < columns.Count(); j++)
                {
                    data[i, j] = columns.ElementAt(j).Value;
                }
            }

            return data;
        }

        private static string TryGetTableName(XElement firstRow)
        {
            IEnumerable<XElement> firstRowColumns = firstRow.Descendants("td");
            if (firstRowColumns.Count() != 1)
            {
                throw new InvalidOperationException($"A first row of the DecisionTable must have only 1 columns. The table currently only have {firstRowColumns.Count()} columns");
            }

            return firstRowColumns.First().Value;
        }
    }
}