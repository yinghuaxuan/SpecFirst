namespace DecisionMarkd.Tests
{
    using System;

    public class type_helper
    {
        public Type GetTypeFromString(string type)
        {
            return type switch
            {
                null => null,
                "int" => typeof(int),
                "int?" => typeof(int?),
                "decimal" => typeof(decimal),
                "decimal?" => typeof(decimal?),
                "bool" => typeof(bool),
                "datetime" => typeof(DateTime),
                "string" => typeof(string),
                _ => typeof(string)
            };
        }
    }
}