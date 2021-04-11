namespace SpecFirst.Core.Converter
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using SpecFirst.Core.DecisionTable;
    using SpecFirst.Core.Serialization;

    public class TableDataToCommentsConverter
    {
        public string[] Convert(TableHeader[] tableHeaders, object[,] decisionData)
        {
            List<string> testData = new List<string>();
            StringBuilder builder = new StringBuilder();

            for (int i = 0; i < decisionData.GetLength(0); i++)
            {
                builder.Clear();
                for (int j = 0; j < decisionData.GetLength(1); j++)
                {
                    if(tableHeaders[j].TableHeaderType == TableHeaderType.Comment)
                    {
                        builder.Append($"{decisionData[i, j]}, ");
                    }
                }

                testData.Add(builder.Remove(builder.Length - 2, 2).ToString());
            }

            return testData.ToArray();
        }
    }
}
