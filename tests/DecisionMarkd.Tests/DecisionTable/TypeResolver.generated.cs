
namespace DecisionMarkd.Tests
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
            infer_type_from_integer_text_implementation(text_value, actual_type);
        }

        public static IEnumerable<object[]> get_test_data()
        {
            var data = new List<object[]>
            {
                new object[] { "0", "int" },
                new object[] { "12", "int" },
                new object[] { "-12", "int" },
                new object[] { "2147483647", "int" },
                new object[] { "-2147483648", "int" },
                new object[] { "2147483648", "decimal" },
                new object[] { "-2147483649", "decimal" },
            };

            return data;
        }

        partial void infer_type_from_integer_text_implementation(String text_value, String actual_type);
    }

    public partial class infer_type_from_decimal_text
    {
        [Theory]
        [MemberData(nameof(get_test_data))]
        public void infer_type_from_decimal_text_tests(String text_value, String actual_type)
        {
            infer_type_from_decimal_text_implementation(text_value, actual_type);
        }

        public static IEnumerable<object[]> get_test_data()
        {
            var data = new List<object[]>
            {
                new object[] { "0.0", "decimal" },
                new object[] { "12.5", "decimal" },
                new object[] { "-12.5", "decimal" },
                new object[] { "2.09550901805872E-05", "decimal" },
                new object[] { "2.09550901805872e-05", "decimal" },
                new object[] { "-2.09550901805872e-05", "decimal" },
                new object[] { "79228162514264337593543950335", "decimal" },
                new object[] { "-79228162514264337593543950335", "decimal" },
                new object[] { "79228162514264337593543950336", "string" },
                new object[] { "-79228162514264337593543950336", "string" },
            };

            return data;
        }

        partial void infer_type_from_decimal_text_implementation(String text_value, String actual_type);
    }

    public partial class infer_type_from_boolean_text
    {
        [Theory]
        [MemberData(nameof(get_test_data))]
        public void infer_type_from_boolean_text_tests(String text_value, String actual_type)
        {
            infer_type_from_boolean_text_implementation(text_value, actual_type);
        }

        public static IEnumerable<object[]> get_test_data()
        {
            var data = new List<object[]>
            {
                new object[] { "true", "bool" },
                new object[] { "true", "bool" },
                new object[] { "true", "bool" },
                new object[] { "true", "bool" },
                new object[] { "false", "bool" },
                new object[] { "false", "bool" },
                new object[] { "false", "bool" },
                new object[] { "false", "bool" },
                new object[] { "Truee", "string" },
                new object[] { "FalSee", "string" },
            };

            return data;
        }

        partial void infer_type_from_boolean_text_implementation(String text_value, String actual_type);
    }

    public partial class infer_type_from_datetime_text
    {
        [Theory]
        [MemberData(nameof(get_test_data))]
        public void infer_type_from_datetime_text_tests(String text_value, String actual_type)
        {
            infer_type_from_datetime_text_implementation(text_value, actual_type);
        }

        public static IEnumerable<object[]> get_test_data()
        {
            var data = new List<object[]>
            {
                new object[] { "2012-12-25 23:59:59", "datetime" },
                new object[] { "25/12/2012 23:59:59", "string" },
                new object[] { "2012-1-1 23:59:59", "string" },
                new object[] { "25 December 2012 23:59:59", "string" },
                new object[] { "0000-12-31 00:00:00", "string" },
                new object[] { "10000-01-01 00:00:01", "string" },
            };

            return data;
        }

        partial void infer_type_from_datetime_text_implementation(String text_value, String actual_type);
    }

    public partial class infer_column_type_from_integer_value
    {
        [Theory]
        [MemberData(nameof(get_test_data))]
        public void infer_column_type_from_integer_value_tests(Int32 text_value, String hint_type, String actual_type)
        {
            infer_column_type_from_integer_value_implementation(text_value, hint_type, actual_type);
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

        partial void infer_column_type_from_integer_value_implementation(Int32 text_value, String hint_type, String actual_type);
    }

    public partial class infer_column_type_from_decimal_value
    {
        [Theory]
        [MemberData(nameof(get_test_data))]
        public void infer_column_type_from_decimal_value_tests(Decimal text_value, String hint_type, String actual_type)
        {
            infer_column_type_from_decimal_value_implementation(text_value, hint_type, actual_type);
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

        partial void infer_column_type_from_decimal_value_implementation(Decimal text_value, String hint_type, String actual_type);
    }

    public partial class infer_column_type_from_bool_value
    {
        [Theory]
        [MemberData(nameof(get_test_data))]
        public void infer_column_type_from_bool_value_tests(Boolean text_value, String hint_type, String actual_type)
        {
            infer_column_type_from_bool_value_implementation(text_value, hint_type, actual_type);
        }

        public static IEnumerable<object[]> get_test_data()
        {
            var data = new List<object[]>
            {
                new object[] { true, null, "bool" },
                new object[] { false, null, "bool" },
                new object[] { true, "bool", "bool" },
                new object[] { false, "bool", "bool" },
                new object[] { true, "bool?", "bool?" },
                new object[] { false, "bool?", "bool?" },
                new object[] { true, "bool", "bool" },
                new object[] { false, "bool", "bool" },
                new object[] { true, "bool", "bool" },
                new object[] { false, "bool", "bool" },
                new object[] { true, "int", "string" },
                new object[] { true, "int?", "string" },
                new object[] { true, "string", "string" },
                new object[] { true, "datetime", "string" },
                new object[] { true, "decimal", "string" },
            };

            return data;
        }

        partial void infer_column_type_from_bool_value_implementation(Boolean text_value, String hint_type, String actual_type);
    }

    public partial class infer_column_type_from_datetime_value
    {
        [Theory]
        [MemberData(nameof(get_test_data))]
        public void infer_column_type_from_datetime_value_tests(String text_value, String hint_type, String actual_type)
        {
            infer_column_type_from_datetime_value_implementation(text_value, hint_type, actual_type);
        }

        public static IEnumerable<object[]> get_test_data()
        {
            var data = new List<object[]>
            {
                new object[] { "2012-12-25 23:59:59", null, "datetime" },
                new object[] { "2012-12-25 23:59:59", "datetime", "datetime" },
                new object[] { "2012-12-25 23:59:59", "datetime?", "datetime?" },
                new object[] { "2012-12-25 23:59:59", "int", "string" },
                new object[] { "2012-12-25 23:59:59", "string", "string" },
                new object[] { "2012-12-25 23:59:59", "decimal", "string" },
                new object[] { "2012-12-25 23:59:59", "bool", "string" },
                new object[] { "25/12/2012 23:59:59", "datetime", "string" },
                new object[] { "2012-1-1 23:59:59", "datetime", "string" },
                new object[] { "25 December 2012 23:59:59", "datetime", "string" },
                new object[] { "0000-12-31 00:00:00", "datetime", "string" },
                new object[] { "10000-01-01 00:00:01", "datetime", "string" },
            };

            return data;
        }

        partial void infer_column_type_from_datetime_value_implementation(String text_value, String hint_type, String actual_type);
    }

}