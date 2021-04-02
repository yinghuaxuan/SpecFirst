namespace SpecFirst.Core.DecisionTable.Parser
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Linq;
    using SpecFirst.Core.TypeResolver;

    public class TableDataParser
    {
        public object[,] Parse(XElement decisionTable, out Type[] types)
        {
            IEnumerable<XElement> rows = decisionTable.Descendants("tr");
            XElement[] dataRows = rows.Skip(2).ToArray();
            string[,] data = GetDecisionData(dataRows);

            types = new Type[data.GetLength(1)];
            object[,] values = new object[data.GetLength(0), data.GetLength(1)];

            for (int i = 0; i < data.GetLength(0); i++)
            {
                for (int j = 0; j < data.GetLength(1); j++)
                {
                    Type type = ScalaValueTypeResolver.Resolve(data[i, j], types[j], out object value);
                    types[j] = type;
                    values[i, j] = value;
                }
            }

            return values;
        }

        private static string[,] GetDecisionData(XElement[] dataRows)
        {
            int numberOfColumns = dataRows.First().Descendants("td").Count();
            string[,] data = new string[dataRows.Length, numberOfColumns];
            for (int i = 0; i < dataRows.Length; i++)
            {
                IEnumerable<XElement> columns = dataRows.ElementAt(i).Descendants("td").ToArray();
                for (int j = 0; j < numberOfColumns; j++)
                {
                    data[i, j] = columns.ElementAt(j).Value;
                }
            }

            return data;
        }
    }


}