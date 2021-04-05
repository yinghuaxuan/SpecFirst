
namespace SpecFirst.Specs.Tests
{
    using System;
    using System.Collections.Generic;
    using Xunit;
    
    public partial class infer_type_from_number_text
    {
        [Theory]
        [MemberData(nameof(get_test_data))]
        public void infer_type_from_number_text_tests(Object number, String actual_type)
        {
            infer_type_from_number_text_implementation(number);
        }

        public static IEnumerable<object[]> get_test_data()
        {
            var data = new List<object[]>
            {
                new object[] { 0, "integer" },
                new object[] { 12, "integer" },
                new object[] { 12, "integer" },
                new object[] { -12, "integer" },
                new object[] { -103E+06, "integer" },
                new object[] { 2147483647, "integer" },
                new object[] { -2147483648, "integer" },
                new object[] { 2147483648D, "double" },
                new object[] { -2147483649D, "double" },
                new object[] { 12.5M, "decimal" },
                new object[] { -12.5M, "decimal" },
                new object[] { 12.5M, "decimal" },
                new object[] { -12.5M, "decimal" },
                new object[] { 7.92281625142643E+28D, "double" },
                new object[] { -7.92281625142643E+28D, "double" },
                new object[] { "79228162514264337593543950336M", "string" },
                new object[] { "-79228162514264337593543950336M", "string" },
                new object[] { 0D, "double" },
                new object[] { 12.5D, "double" },
                new object[] { -12.5D, "double" },
                new object[] { -12.5D, "double" },
                new object[] { 12.5D, "double" },
                new object[] { -12.5D, "double" },
                new object[] { 12.5D, "double" },
                new object[] { -12.5D, "double" },
                new object[] { -0.0000209550901805872M, "decimal" },
                new object[] { -209550.901805872M, "decimal" },
                new object[] { -2.09550901805872E-05D, "double" },
                new object[] { -209550.901805872D, "double" },
                new object[] { "2.7976931348623157E+308", "string" },
                new object[] { "-2.7976931348623157E+308", "string" },
                new object[] { 12, "integer" },
                new object[] { 12, "integer" },
                new object[] { "-2.7976931348623157E", "string" },
                new object[] { "-2.", "string" },
            };

            return data;
        }

        private partial String infer_type_from_number_text_implementation(Object number);
    }

    public partial class infer_type_from_boolean_text
    {
        [Theory]
        [MemberData(nameof(get_test_data))]
        public void infer_type_from_boolean_text_tests(Object text_value, String actual_type)
        {
            infer_type_from_boolean_text_implementation(text_value);
        }

        public static IEnumerable<object[]> get_test_data()
        {
            var data = new List<object[]>
            {
                new object[] { true, "bool" },
                new object[] { true, "bool" },
                new object[] { true, "bool" },
                new object[] { true, "bool" },
                new object[] { false, "bool" },
                new object[] { false, "bool" },
                new object[] { false, "bool" },
                new object[] { false, "bool" },
                new object[] { "Truee", "string" },
                new object[] { "FalSee", "string" },
            };

            return data;
        }

        private partial String infer_type_from_boolean_text_implementation(Object text_value);
    }

    public partial class infer_type_from_datetime_text
    {
        [Theory]
        [MemberData(nameof(get_test_data))]
        public void infer_type_from_datetime_text_tests(Object text_value, String actual_type)
        {
            infer_type_from_datetime_text_implementation(text_value);
        }

        public static IEnumerable<object[]> get_test_data()
        {
            var data = new List<object[]>
            {
                new object[] { new DateTime(2012, 12, 25, 23, 59, 59, 0), "datetime" },
                new object[] { "25/12/2012 23:59:59", "string" },
                new object[] { "2012-1-1 23:59:59", "string" },
                new object[] { "25 December 2012 23:59:59", "string" },
                new object[] { "0000-12-31 00:00:00", "string" },
                new object[] { "10000-01-01 00:00:01", "string" },
            };

            return data;
        }

        private partial String infer_type_from_datetime_text_implementation(Object text_value);
    }

}