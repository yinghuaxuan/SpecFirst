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

            object[,] values = new object[data.GetLength(0), data.GetLength(1)];
            Type[,] dataTypes = new Type[data.GetLength(0), data.GetLength(1)];

            for (int i = 0; i < data.GetLength(0); i++)
            {
                for (int j = 0; j < data.GetLength(1); j++)
                {
                    Type type = ScalaValueTypeResolver.Resolve(data[i, j], out object value);
                    dataTypes[i, j] = type;
                    values[i, j] = value;
                }
            }

            types = GetColumnTypes(dataTypes);
            return values;
        }

        private Type[] GetColumnTypes(Type[,] dataTypes)
        {
            var columns = dataTypes.GetLength(1);
            var columnTypes = new Type[columns];
            for (int i = 0; i < columns; i++)
            {
                columnTypes[i] = CollectionTypeResolver.Resolve(GetColumn(dataTypes, i));
            }

            return columnTypes;
        }

        public static Type[] GetColumn(Type[,] dataTypes, int column)
        {
            var rows = dataTypes.GetLength(0);
            var array = new Type[rows];
            for (int i = 0; i < rows; ++i)
            {
                array[i] = dataTypes[i, column];
            }
            return array;
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