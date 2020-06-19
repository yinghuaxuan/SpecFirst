namespace DecisionMarkd.DecisionTable.Parser
{
    using DecisionMarkd.DecisionTable;
    using System;

    public class TableDataParser
    {
        public object[,] Parse(string[,] data, out Type[] types)
        {
            types = new Type[data.GetLength(1)];
            object[,] values = new object[data.GetLength(0), data.GetLength(1)];

            for (int i = 0; i < data.GetLength(0); i++)
            {
                for (int j = 0; j < data.GetLength(1); j++)
                {
                    Type type = TypeResolver.InferTypeFromText(data[i, j], types[j], out object value);
                    types[j] = type;
                    values[i, j] = value;
                }
            }

            return values;
        }
    }


}