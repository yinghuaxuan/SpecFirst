
namespace DecisionMarkd.Tests
{
    using System;
    using DecisionMarkd.DecisionTable;
    using Xunit;

    public partial class infer_type_from_integer_text : type_helper
    {
        partial void infer_type_from_integer_text_implementation(String text_value, String actual_type)
        {
            Type type = TypeResolver.InferTypeFromText(text_value.ToString(), null, out _);
            Assert.Equal(GetTypeFromString(actual_type), type);
        }
    }

    public partial class infer_type_from_decimal_text : type_helper
    {
        partial void infer_type_from_decimal_text_implementation(String text_value, String actual_type)
        {
            Type type = TypeResolver.InferTypeFromText(text_value.ToString(), null, out _);
            Assert.Equal(GetTypeFromString(actual_type), type);
        }
    }

    public partial class infer_type_from_boolean_text : type_helper
    {
        partial void infer_type_from_boolean_text_implementation(String text_value, String actual_type)
        {
            Type type = TypeResolver.InferTypeFromText(text_value.ToString(), null, out _);
            Assert.Equal(GetTypeFromString(actual_type), type);
        }
    }

    public partial class infer_type_from_datetime_text : type_helper
    {
        partial void infer_type_from_datetime_text_implementation(String text_value, String actual_type)
        {
            Type type = TypeResolver.InferTypeFromText(text_value.ToString(), null, out _);
            Assert.Equal(GetTypeFromString(actual_type), type);
        }
    }

    public partial class infer_column_type_from_integer_value : type_helper
    {
        partial void infer_column_type_from_integer_value_implementation(Int32 text_value, String hint_type, String actual_type)
        {
            Type type = TypeResolver.InferTypeFromText(text_value.ToString(), GetTypeFromString(hint_type), out _);
            Assert.Equal(GetTypeFromString(actual_type), type);
        }
    }

    public partial class infer_column_type_from_decimal_value : type_helper
    {
        partial void infer_column_type_from_decimal_value_implementation(Decimal text_value, String hint_type, String actual_type)
        {
            Type type = TypeResolver.InferTypeFromText(text_value.ToString(), GetTypeFromString(hint_type), out _);
            Assert.Equal(GetTypeFromString(actual_type), type);
        }
    }

    public partial class infer_column_type_from_bool_value : type_helper
    {
        partial void infer_column_type_from_bool_value_implementation(Boolean text_value, String hint_type, String actual_type)
        {
            Type type = TypeResolver.InferTypeFromText(text_value.ToString(), GetTypeFromString(hint_type), out _);
            Assert.Equal(GetTypeFromString(actual_type), type);
        }
    }

    public partial class infer_column_type_from_datetime_value : type_helper
    {
        partial void infer_column_type_from_datetime_value_implementation(String text_value, String hint_type, String actual_type)
        {
            Type type = TypeResolver.InferTypeFromText(text_value.ToString(), GetTypeFromString(hint_type), out _);
            Assert.Equal(GetTypeFromString(actual_type), type);
        }
    }

}