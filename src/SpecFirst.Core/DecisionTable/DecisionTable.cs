namespace SpecFirst.Core.DecisionTable
{
    public sealed class DecisionTable
    {
        public DecisionTable(string fixtureName, TableHeader[] variables, object[,] tableData)
        {
            TableName = fixtureName;
            TableHeaders = variables;
            TableData = tableData;
        }

        public string TableName { get; }
        public TableHeader[] TableHeaders { get; }
        public object[,] TableData { get; }
    }
}