
namespace SpecFirst.Specs
{
    using System;
    using SpecFirst.Core.TypeResolver;

    public partial class infer_type_from_number_text
    {
        private partial (string, string) infer_type_from_number_text_implementation(string text_value)
        {
            var type = TypeResolver.Resolve(text_value, out var parsedValue);
            return (TypeHelper.GetTypeString(type), TypeHelper.Serialize(parsedValue, type));
        }
    }

    public partial class infer_type_from_boolean_text
    {
        private partial (string, string) infer_type_from_boolean_text_implementation(string text_value)
        {
            var type = TypeResolver.Resolve(text_value, out var parsedValue);
            return (TypeHelper.GetTypeString(type), TypeHelper.Serialize(parsedValue, type));
        }
    }

    public partial class infer_type_from_datetime_text
    {
        private partial (string, string) infer_type_from_datetime_text_implementation(string text_value)
        {
            var type = TypeResolver.Resolve(text_value, out var parsedValue);
            return (TypeHelper.GetTypeString(type), TypeHelper.Serialize(parsedValue, type));
        }
    }

    public partial class infer_type_from_string_text
    {
        private partial (string, string) infer_type_from_string_text_implementation(string text_value)
        {
            var type = TypeResolver.Resolve(text_value, out var parsedValue);
            return (TypeHelper.GetTypeString(type), parsedValue.ToString());
        }
    }

}