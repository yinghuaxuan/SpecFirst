namespace DecisionMarkd
{
    using System.Collections.Generic;

    public sealed class DecisionTable
    {
        public DecisionTable(string className, string parameters, IList<string> testData)
        {
            ClassName = className;
            Parameters = parameters;
            TestData = testData;
        }

        public string ClassName { get; }
        public string Parameters { get; }
        public IList<string> TestData { get; }
    }
}