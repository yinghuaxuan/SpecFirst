
namespace SpecFirst.Specs.Tests
{
    using System;
    using System.Collections.Generic;
    using Xunit;
    
    public partial class infer_type_from_number_text
    {
        [Theory]
        [MemberData(nameof(get_test_data))]
        public void infer_type_from_number_text_tests(object number, string actual_type, string parsed_number)
        {
            (string actual_type_output, string parsed_number_output) = infer_type_from_number_text_implementation(number);
            Assert.Equal(actual_type_output, actual_type);
Assert.Equal(parsed_number_output, parsed_number);

        }

        public static IEnumerable<object[]> get_test_data()
        {
            var data = new List<object[]>
            {
                new object[] { 0, "integer", "0" }, // integer
                new object[] { 12, "integer", "12" }, // integer
                new object[] { 12, "integer", "12" }, // positive integer
                new object[] { -12, "integer", "-12" }, // negative integer
                new object[] { -103E+06, "integer", "-103E+06" }, // integer with exponent notation
                new object[] { 2147483647, "integer", "2147483647" }, // integer, max value
                new object[] { -2147483648, "integer", "-2147483648" }, // integer, min value
                new object[] { 2147483648D, "double", "2147483648" }, // integer, max value + 1
                new object[] { -2147483649D, "double", "-2147483649" }, // integer, min value - 1
                new object[] { 12.5M, "decimal", "12.5M" }, // decimal notation
                new object[] { -12.5M, "decimal", "-12.5M" }, // decimal notation
                new object[] { 12.5M, "decimal", "12.5m" }, // decimal notation
                new object[] { -12.5M, "decimal", "-12.5m" }, // decimal notation
                new object[] { 79228162514264337593543950336D, "double", "79228162514264337593543950336D" }, // decimal, max value + 1
                new object[] { -79228162514264337593543950336D, "double", "-79228162514264337593543950336D" }, // decimal, min value - 1
                new object[] { "79228162514264337593543950336M", "string", "79228162514264337593543950336M" }, // decimal notation, max value + 1
                new object[] { "-79228162514264337593543950336M", "string", "-79228162514264337593543950336M" }, // decimal notation, min value - 1
                new object[] { 0.0D, "double", "0.0" }, // double
                new object[] { 12.5D, "double", "12.5" }, // double
                new object[] { -12.5D, "double", "-12.5" }, // positive double
                new object[] { -12.5D, "double", "-12.5" }, // negative double
                new object[] { 12.5D, "double", "12.5D" }, // double notation
                new object[] { -12.5D, "double", "-12.5D" }, // double notation
                new object[] { 12.5D, "double", "12.5D" }, // double notation
                new object[] { -12.5D, "double", "-12.5D" }, // double notation
                new object[] { -2.09550901805872E-05M, "decimal", "-2.09550901805872E-05M" }, // decimal with exponent notation
                new object[] { -2.09550901805872E+05M, "decimal", "-2.09550901805872E+05M" }, // decimal with exponent notation
                new object[] { -2.09550901805872E-05D, "double", "-2.09550901805872E-05D" }, // double with exponent notation
                new object[] { -2.09550901805872E+05D, "double", "-2.09550901805872E+05D" }, // double with exponent notation
                new object[] { "2.7976931348623157E+308", "string", "2.7976931348623157E+308" }, // double, beyond max value
                new object[] { "-2.7976931348623157E+308", "string", "-2.7976931348623157E+308" }, // double, below min value
                new object[] { 12, "integer", "12" }, // leading spaces not considered
                new object[] { 12, "integer", "12" }, // trailing spaces not considered
                new object[] { "-2.7976931348623157E", "string", "-2.7976931348623157E" }, // not valid number
                new object[] { "-2.", "string", "-2." }, // not valid number
            };

            return data;
        }

        private partial (string, string) infer_type_from_number_text_implementation(object number);
    }

    public partial class infer_type_from_boolean_text
    {
        [Theory]
        [MemberData(nameof(get_test_data))]
        public void infer_type_from_boolean_text_tests(object text_value, string actual_type)
        {
            string actual_type_output = infer_type_from_boolean_text_implementation(text_value);
            Assert.Equal(actual_type_output, actual_type);

        }

        public static IEnumerable<object[]> get_test_data()
        {
            var data = new List<object[]>
            {
                new object[] { true, "bool" }, // bool, camel case
                new object[] { true, "bool" }, // bool, lower case
                new object[] { true, "bool" }, // bool, upper case
                new object[] { true, "bool" }, // bool, mixed case
                new object[] { false, "bool" }, // bool, camel case
                new object[] { false, "bool" }, // bool, lower case
                new object[] { false, "bool" }, // bool, upper case
                new object[] { false, "bool" }, // bool, mixed case
                new object[] { "Truee", "string" }, // not valid boolean text
                new object[] { "FalSee", "string" }, // not valid boolean text
            };

            return data;
        }

        private partial string infer_type_from_boolean_text_implementation(object text_value);
    }

    public partial class infer_type_from_datetime_text
    {
        [Theory]
        [MemberData(nameof(get_test_data))]
        public void infer_type_from_datetime_text_tests(object text_value, string actual_type)
        {
            string actual_type_output = infer_type_from_datetime_text_implementation(text_value);
            Assert.Equal(actual_type_output, actual_type);

        }

        public static IEnumerable<object[]> get_test_data()
        {
            var data = new List<object[]>
            {
                new object[] { new DateTime(2012, 12, 25, 23, 59, 59, 0), "datetime" }, // datetime in yyyy-MM-dd HH:mm:ss
                new object[] { "25/12/2012 23:59:59", "string" }, // datetime, not supported format
                new object[] { "2012-1-1 23:59:59", "string" }, // datetime, not supported format
                new object[] { "25 December 2012 23:59:59", "string" }, // datetime, not supported format
                new object[] { "0000-12-31 00:00:00", "string" }, // datetime, out of range (min)
                new object[] { "10000-01-01 00:00:01", "string" }, // datetime, out of range (max)
            };

            return data;
        }

        private partial string infer_type_from_datetime_text_implementation(object text_value);
    }

}