namespace DecisionMarkd.DecisionTable.Parser
{
    using System.Collections.Generic;

    public class TableNameParser
    {
        public static readonly HashSet<char> InvalidCharacters = new HashSet<char> { };
        public string Convert(string tableName)
        {
            // should escape all invalid characters for a class name such as .
            return tableName.Replace(" ", "_").ToLowerInvariant();
        }
    }
}