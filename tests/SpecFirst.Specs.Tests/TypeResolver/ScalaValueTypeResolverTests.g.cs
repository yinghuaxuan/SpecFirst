
namespace SpecFirst.Specs.Tests
{
    using System;
    using System.Collections.Generic;
    using Xunit;
    
    public partial class infer_type_from_number_text
    {
        [Theory]
        [MemberData(nameof(get_test_data))]
        public void infer_type_from_number_text_tests(string text_value, string actual_type, string serialized_value)
        {
            (string actual_type_output, string serialized_value_output) = infer_type_from_number_text_implementation(text_value);
            Assert.Equal(actual_type_output, actual_type);
            Assert.Equal(serialized_value_output, serialized_value);
        }

        public static IEnumerable<object[]> get_test_data()
        {
            var data = new List<object[]>
            {
                new object[] { "0", "integer", "0" }, // integer
                new object[] { "12", "integer", "12" }, // integer
                new object[] { "+12", "integer", "12" }, // positive integer
                new object[] { "-12", "integer", "-12" }, // negative integer
                new object[] { "-103E+06", "integer", "-103E+06" }, // integer with exponent notation
                new object[] { "2147483647", "integer", "2147483647" }, // integer, max value
                new object[] { "-2147483648", "integer", "-2147483648" }, // integer, min value
                new object[] { "2147483648", "double", "2147483648D" }, // integer, max value + 1
                new object[] { "-2147483649", "double", "-2147483649D" }, // integer, min value - 1
                new object[] { "12.5M", "decimal", "12.5M" }, // decimal notation
                new object[] { "-12.5M", "decimal", "-12.5M" }, // decimal notation
                new object[] { "12.5m", "decimal", "12.5M" }, // decimal notation
                new object[] { "-12.5m", "decimal", "-12.5M" }, // decimal notation
                new object[] { "79228162514264337593543950336", "double", "79228162514264337593543950336D" }, // decimal, max value + 1
                new object[] { "-79228162514264337593543950336", "double", "-79228162514264337593543950336D" }, // decimal, min value - 1
                new object[] { "79228162514264337593543950336M", "string", "79228162514264337593543950336M" }, // decimal notation, max value + 1
                new object[] { "-79228162514264337593543950336M", "string", "-79228162514264337593543950336M" }, // decimal notation, min value - 1
                new object[] { "0.0", "double", "0.0D" }, // double
                new object[] { "12.5", "double", "12.5D" }, // double
                new object[] { "-12.5", "double", "-12.5D" }, // positive double
                new object[] { "-12.5", "double", "-12.5D" }, // negative double
                new object[] { "12.5D", "double", "12.5D" }, // double notation
                new object[] { "-12.5D", "double", "-12.5D" }, // double notation
                new object[] { "12.5d", "double", "12.5D" }, // double notation
                new object[] { "-12.5d", "double", "-12.5D" }, // double notation
                new object[] { "-2.09550901805872E-05M", "decimal", "-2.09550901805872E-05M" }, // decimal with exponent notation
                new object[] { "-2.09550901805872E+05M", "decimal", "-2.09550901805872E+05M" }, // decimal with exponent notation
                new object[] { "-2.09550901805872E-05D", "double", "-2.09550901805872E-05D" }, // double with exponent notation
                new object[] { "-2.09550901805872E+05D", "double", "-2.09550901805872E+05D" }, // double with exponent notation
                new object[] { "2.7976931348623157E+308", "string", "2.7976931348623157E+308" }, // double, beyond max value
                new object[] { "-2.7976931348623157E+308", "string", "-2.7976931348623157E+308" }, // double, below min value
                new object[] { "   12", "integer", "12" }, // leading spaces not considered
                new object[] { "12   ", "integer", "12" }, // trailing spaces not considered
                new object[] { "-2.7976931348623157E", "string", "-2.7976931348623157E" }, // not valid number
                new object[] { "-2.", "string", "-2." }, // not valid number
            };

            return data;
        }

        private partial (string, string) infer_type_from_number_text_implementation(string text_value);
    }

    public partial class infer_type_from_boolean_text
    {
        [Theory]
        [MemberData(nameof(get_test_data))]
        public void infer_type_from_boolean_text_tests(string text_value, string actual_type, string serialized_value)
        {
            (string actual_type_output, string serialized_value_output) = infer_type_from_boolean_text_implementation(text_value);
            Assert.Equal(actual_type_output, actual_type);
            Assert.Equal(serialized_value_output, serialized_value);
        }

        public static IEnumerable<object[]> get_test_data()
        {
            var data = new List<object[]>
            {
                new object[] { "True", "bool", "true" }, // bool, camel case
                new object[] { "true", "bool", "true" }, // bool, lower case
                new object[] { "TRUE", "bool", "true" }, // bool, upper case
                new object[] { "TrUe", "bool", "true" }, // bool, mixed case
                new object[] { "False", "bool", "false" }, // bool, camel case
                new object[] { "false", "bool", "false" }, // bool, lower case
                new object[] { "FALSE", "bool", "false" }, // bool, upper case
                new object[] { "FalSe", "bool", "false" }, // bool, mixed case
                new object[] { "Truee", "string", "Truee" }, // not valid boolean text
                new object[] { "FalSee", "string", "FalSee" }, // not valid boolean text
            };

            return data;
        }

        private partial (string, string) infer_type_from_boolean_text_implementation(string text_value);
    }

    public partial class infer_type_from_datetime_text
    {
        [Theory]
        [MemberData(nameof(get_test_data))]
        public void infer_type_from_datetime_text_tests(string text_value, string actual_type, string serialized_value)
        {
            (string actual_type_output, string serialized_value_output) = infer_type_from_datetime_text_implementation(text_value);
            Assert.Equal(actual_type_output, actual_type);
            Assert.Equal(serialized_value_output, serialized_value);
        }

        public static IEnumerable<object[]> get_test_data()
        {
            var data = new List<object[]>
            {
                new object[] { "2012-12-25 23:59:59", "datetime", "new DateTime(2012, 12, 25, 23, 59, 59, 0)" }, // datetime in yyyy-MM-dd HH:mm:ss
                new object[] { "25/12/2012 23:59:59", "string", "25/12/2012 23:59:59" }, // datetime, not supported format
                new object[] { "2012-1-1 23:59:59", "string", "2012-1-1 23:59:59" }, // datetime, not supported format
                new object[] { "25 December 2012 23:59:59", "string", "25 December 2012 23:59:59" }, // datetime, not supported format
                new object[] { "0000-12-31 00:00:00", "string", "0000-12-31 00:00:00" }, // datetime, out of range (min)
                new object[] { "10000-01-01 00:00:01", "string", "10000-01-01 00:00:01" }, // datetime, out of range (max)
            };

            return data;
        }

        private partial (string, string) infer_type_from_datetime_text_implementation(string text_value);
    }

    public partial class infer_type_from_string_text
    {
        [Theory]
        [MemberData(nameof(get_test_data))]
        public void infer_type_from_string_text_tests(string text_value, string actual_type, string serialized_value)
        {
            (string actual_type_output, string serialized_value_output) = infer_type_from_string_text_implementation(text_value);
            Assert.Equal(actual_type_output, actual_type);
            Assert.Equal(serialized_value_output, serialized_value);
        }

        public static IEnumerable<object[]> get_test_data()
        {
            var data = new List<object[]>
            {
                new object[] { "this is a string", "string", "this is a string" }, // string without quote
                new object[] { "this is a string", "string", "this is a string" }, // fully quoted string is same as string without quote
                new object[] { "\"this is a string\"", "string", "\"this is a string\"" }, // escape fully quoted string to keep the quotes
                new object[] { "\"this is a string", "string", "\"this is a string" }, // half quoted string will keep the quotes
                new object[] { "this is a string\"", "string", "this is a string\"" }, // half quoted string will keep the quotes
                new object[] { "this is a \"string\"", "string", "this is a \"string\"" }, // string with quotes in it
                new object[] { "this is a \"string\"", "string", "this is a \"string\"" }, // string with nested quotes in it
                new object[] { "this is a \"string", "string", "this is a \"string" }, // string with one quote in it
                new object[] { "this is a \\string", "string", "this is a \\string" }, // string with a backslash in it
                new object[] { "this is a \"string", "string", "this is a \"string" }, // string with escape characters
                new object[] { "this is a \"string", "string", "this is a \"string" }, // quoted string with escape characters
                new object[] { "this is a <string>", "string", "this is a <string>" }, // string with great than and less than character
                new object[] { "  ", "string", "  " }, // white spaces will be kept
            };

            return data;
        }

        private partial (string, string) infer_type_from_string_text_implementation(string text_value);
    }

}