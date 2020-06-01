
namespace DecisionMarkd.Tests
{
    using System;
    using DecisionMarkd.DecisionTable;
    using Xunit;

    public partial class infer_type_from_value
    {
        partial void infer_type_from_value_implementation(Int32 value_in_string, String hint_type, String actual_type)
        {
            Type type = TypeHelper.InferTypeFromValue(value_in_string.ToString(), GetTypeFromString(hint_type), out _);
            Assert.Equal(type, GetTypeFromString(actual_type));
        }

        public Type GetTypeFromString(string type)
        {
            return type switch
            {
                "int" => typeof(int),
                "decimal" => typeof(decimal),
                "bool" => typeof(bool),
                "date" => typeof(DateTime),
                "string" => typeof(string),
                _ => throw new InvalidOperationException()
            };
        }
    }
}
