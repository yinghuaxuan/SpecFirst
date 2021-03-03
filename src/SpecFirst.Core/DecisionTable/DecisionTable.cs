namespace SpecFirst.Core.DecisionTable
{
    public sealed class DecisionTable
    {
        public DecisionTable(string fixtureName, TableHeader[] variables, object[,] decisionData)
        {
            TableName = fixtureName;
            TableHeaders = variables;
            TableData = decisionData;
        }

        public string TableName { get; }
        public TableHeader[] TableHeaders { get; }
        public object[,] TableData { get; }
    }
}