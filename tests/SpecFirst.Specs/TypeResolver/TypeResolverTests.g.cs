
namespace SpecFirst.Specs.Tests
{
    using System;
    using System.Collections.Generic;
    using Xunit;
    
    public partial class infer_type_from_integer_text
    {
        [Theory]
        [MemberData(nameof(get_test_data))]
        public void infer_type_from_integer_text_tests(String text_value, String actual_type)
        {
            infer_type_from_integer_text_implementation(text_value);
        }

        public static IEnumerable<object[]> get_test_data()
        {
            var data = new List<object[]>
            {
                new object[] { 0 },
                new object[] { 12 },
                new object[] { -12 },
                new object[] { 2147483647 },
                new object[] { -2147483648 },
                new object[] { 2147483648 },
                new object[] { -2147483649 },
                new object[] { "79228162514264337593543950336" },
                new object[] { "-79228162514264337593543950336" },
            };

            return data;
        }

        private partial String infer_type_from_integer_text_implementation(String text_value);
    }

    public partial class infer_type_from_decimal_text
    {
        [Theory]
        [MemberData(nameof(get_test_data))]
        public void infer_type_from_decimal_text_tests(String text_value, String actual_type)
        {
            infer_type_from_decimal_text_implementation(text_value);
        }

        public static IEnumerable<object[]> get_test_data()
        {
            var data = new List<object[]>
            {
                new object[] { 0.0 },
                new object[] { 12.5 },
                new object[] { -12.5 },
                new object[] { 0.0000209550901805872 },
                new object[] { 0.0000209550901805872 },
                new object[] { -0.0000209550901805872 },
                new object[] { 79228162514264337593543950335M },
                new object[] { -79228162514264337593543950335M },
                new object[] { "79228162514264337593543950336" },
                new object[] { "-79228162514264337593543950336" },
            };

            return data;
        }

        private partial String infer_type_from_decimal_text_implementation(String text_value);
    }

    public partial class infer_type_from_boolean_text
    {
        [Theory]
        [MemberData(nameof(get_test_data))]
        public void infer_type_from_boolean_text_tests(String text_value, String actual_type)
        {
            infer_type_from_boolean_text_implementation(text_value);
        }

        public static IEnumerable<object[]> get_test_data()
        {
            var data = new List<object[]>
            {
                new object[] { true },
                new object[] { true },
                new object[] { true },
                new object[] { true },
                new object[] { false },
                new object[] { false },
                new object[] { false },
                new object[] { false },
                new object[] { "Truee" },
                new object[] { "FalSee" },
            };

            return data;
        }

        private partial String infer_type_from_boolean_text_implementation(String text_value);
    }

    public partial class infer_type_from_datetime_text
    {
        [Theory]
        [MemberData(nameof(get_test_data))]
        public void infer_type_from_datetime_text_tests(String text_value, String actual_type)
        {
            infer_type_from_datetime_text_implementation(text_value);
        }

        public static IEnumerable<object[]> get_test_data()
        {
            var data = new List<object[]>
            {
                new object[] { new DateTime(2012, 12, 25, 1, 1, 1, 0) },
                new object[] { "25/12/2012 23:59:59" },
                new object[] { "2012-1-1 23:59:59" },
                new object[] { "25 December 2012 23:59:59" },
                new object[] { "0000-12-31 00:00:00" },
                new object[] { "10000-01-01 00:00:01" },
            };

            return data;
        }

        private partial String infer_type_from_datetime_text_implementation(String text_value);
    }

    public partial class infer_column_type_from_integer_values
    {
        [Theory]
        [MemberData(nameof(get_test_data))]
        public void infer_column_type_from_integer_values_tests(Int32 text_value, String actual_type)
        {
            infer_column_type_from_integer_values_implementation(text_value);
        }

        public static IEnumerable<object[]> get_test_data()
        {
            var data = new List<object[]>
            {
                new object[] { 12 },
                new object[] { 12 },
                new object[] { 12 },
                new object[] { 12 },
                new object[] { 12 },
                new object[] { 12 },
                new object[] { 12 },
                new object[] { 12 },
            };

            return data;
        }

        private partial String infer_column_type_from_integer_values_implementation(Int32 text_value);
    }

    public partial class infer_column_type_from_decimal_values
    {
        [Theory]
        [MemberData(nameof(get_test_data))]
        public void infer_column_type_from_decimal_values_tests(Decimal text_value, String actual_type)
        {
            infer_column_type_from_decimal_values_implementation(text_value);
        }

        public static IEnumerable<object[]> get_test_data()
        {
            var data = new List<object[]>
            {
                new object[] { 12.5 },
                new object[] { 12.5 },
                new object[] { 12.5 },
                new object[] { 12.5 },
                new object[] { 12.5 },
                new object[] { 12.5 },
                new object[] { 12.5 },
                new object[] { 12.5 },
            };

            return data;
        }

        private partial String infer_column_type_from_decimal_values_implementation(Decimal text_value);
    }

    public partial class infer_column_type_from_bool_value
    {
        [Theory]
        [MemberData(nameof(get_test_data))]
        public void infer_column_type_from_bool_value_tests(Boolean text_value, String actual_type)
        {
            infer_column_type_from_bool_value_implementation(text_value);
        }

        public static IEnumerable<object[]> get_test_data()
        {
            var data = new List<object[]>
            {
                new object[] { true },
                new object[] { false },
                new object[] { true },
                new object[] { false },
                new object[] { true },
                new object[] { false },
                new object[] { true },
                new object[] { false },
                new object[] { true },
                new object[] { false },
                new object[] { true },
                new object[] { true },
                new object[] { true },
                new object[] { true },
                new object[] { true },
            };

            return data;
        }

        private partial String infer_column_type_from_bool_value_implementation(Boolean text_value);
    }

    public partial class infer_column_type_from_datetime_value
    {
        [Theory]
        [MemberData(nameof(get_test_data))]
        public void infer_column_type_from_datetime_value_tests(String text_value, String actual_type)
        {
            infer_column_type_from_datetime_value_implementation(text_value);
        }

        public static IEnumerable<object[]> get_test_data()
        {
            var data = new List<object[]>
            {
                new object[] { new DateTime(2012, 12, 25) },
                new object[] { new DateTime(2012, 12, 25) },
                new object[] { new DateTime(2012, 12, 25) },
                new object[] { new DateTime(2012, 12, 25) },
                new object[] { new DateTime(2012, 12, 25) },
                new object[] { new DateTime(2012, 12, 25) },
                new object[] { new DateTime(2012, 12, 25) },
                new object[] { "25/12/2012 23:59:59" },
                new object[] { "2012-1-1 23:59:59" },
                new object[] { "25 December 2012 23:59:59" },
                new object[] { "0000-12-31 00:00:00" },
                new object[] { "10000-01-01 00:00:01" },
            };

            return data;
        }

        private partial String infer_column_type_from_datetime_value_implementation(String text_value);
    }

}