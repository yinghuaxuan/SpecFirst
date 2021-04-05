
namespace SpecFirst.Specs.Tests
{
    using System;

    public partial class infer_type_from_number_text
    {
        private partial String infer_type_from_number_text_implementation(Object number)
        {
            return TypeHelper.GetTypeString(number.GetType());
        }
    }

    public partial class infer_type_from_boolean_text
    {
        private partial String infer_type_from_boolean_text_implementation(Object number)
        {
            return TypeHelper.GetTypeString(number.GetType());

        }
    }

    public partial class infer_type_from_datetime_text
    {
        private partial String infer_type_from_datetime_text_implementation(Object number)
        {
            return TypeHelper.GetTypeString(number.GetType());
        }
    }

}