namespace DecisionMarkd.DecisionTable
{
    public sealed class DecisionTable
    {
        public DecisionTable(string fixtureName, DecisionVariable[] variables, object[,] decisionData)
        {
            FixtureName = fixtureName;
            Variables = variables;
            DecisionData = decisionData;
        }

        public string FixtureName { get; }
        public DecisionVariable[] Variables { get; }
        public object[,] DecisionData { get; }
    }
}