
namespace SpecFirst.Specs.Tests
{
    using System;
    using System.Collections.Generic;
    using Xunit;
    
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