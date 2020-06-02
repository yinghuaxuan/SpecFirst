
namespace DecisionMarkd.Specs
{
    using System;
    using System.Collections.Generic;
    using Xunit;
    
    public partial class infer_type_from_integer_value
    {
        [Theory]
        [MemberData(nameof(get_test_data))]
        public void infer_type_from_integer_value_tests(Int32 value_in_string, String hint_type, String actual_type)
        {
            infer_type_from_integer_value_implementation(value_in_string, hint_type, actual_type);
        }

        public static IEnumerable<object[]> get_test_data()
        {
            var data = new List<object[]>
            {
                new object[] { 12, null, "int" },
                new object[] { 12, "int", "int" },
                new object[] { 12, "int?", "int?" },
                new object[] { 12, "decimal", "decimal" },
                new object[] { 12, "decimal?", "decimal?" },
                new object[] { 12, "string", "string" },
                new object[] { 12, "datetime", "string" },
                new object[] { 12, "bool", "string" },
            };

            return data;
        }

        partial void infer_type_from_integer_value_implementation(Int32 value_in_string, String hint_type, String actual_type);
    }

    public partial class infer_type_from_decimal_value
    {
        [Theory]
        [MemberData(nameof(get_test_data))]
        public void infer_type_from_decimal_value_tests(Decimal value_in_string, String hint_type, String actual_type)
        {
            infer_type_from_decimal_value_implementation(value_in_string, hint_type, actual_type);
        }

        public static IEnumerable<object[]> get_test_data()
        {
            var data = new List<object[]>
            {
                new object[] { 12.5, null, "decimal" },
                new object[] { 12.5, "decimal", "decimal" },
                new object[] { 12.5, "decimal?", "decimal?" },
                new object[] { 12.5, "int", "decimal" },
                new object[] { 12.5, "int?", "decimal?" },
                new object[] { 12.5, "string", "string" },
                new object[] { 12.5, "datetime", "string" },
                new object[] { 12.5, "bool", "string" },
            };

            return data;
        }

        partial void infer_type_from_decimal_value_implementation(Decimal value_in_string, String hint_type, String actual_type);
    }

}