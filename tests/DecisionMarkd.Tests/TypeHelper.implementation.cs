
namespace DecisionMarkd.Specs
{
    using DecisionMarkd.DecisionTable;
    using System;
    using Xunit;

    public partial class infer_type_from_integer_value : type_helper
    {
        partial void infer_type_from_integer_value_implementation(Int32 value_in_string, String hint_type, String actual_type)
        {
            Type type = TypeHelper.InferTypeFromValue(value_in_string.ToString(), GetTypeFromString(hint_type), out _);
            Assert.Equal(type, GetTypeFromString(actual_type));
        }
    }

    public partial class infer_type_from_decimal_value : type_helper
    {
        partial void infer_type_from_decimal_value_implementation(Decimal value_in_string, String hint_type, String actual_type)
        {
            Type type = TypeHelper.InferTypeFromValue(value_in_string.ToString(), GetTypeFromString(hint_type), out _);
            Assert.Equal(type, GetTypeFromString(actual_type));
        }
    }

    public partial class infer_type_from_bool_value : type_helper
    {
        partial void infer_type_from_bool_value_implementation(Boolean value_in_string, String hint_type, String actual_type)
        {
            Type type = TypeHelper.InferTypeFromValue(value_in_string.ToString(), GetTypeFromString(hint_type), out _);
            Assert.Equal(type, GetTypeFromString(actual_type));
        }
    }

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
