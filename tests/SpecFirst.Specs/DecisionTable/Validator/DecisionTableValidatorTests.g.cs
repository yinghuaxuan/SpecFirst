
namespace SpecFirst.Specs
{
    using System;
    using System.Collections.Generic;
    using Xunit;
    
    public partial class decision_table_validator
    {
        [Theory]
        [MemberData(nameof(get_test_data))]
        public void decision_table_validator_tests(string decision_table, bool is_valid, string validation_error)
        {
            (bool is_valid_output, string validation_error_output) = decision_table_validator_implementation(decision_table);
            Assert.Equal(is_valid_output, is_valid);
            Assert.Equal(validation_error_output, validation_error);
        }

        public static IEnumerable<object[]> get_test_data()
        {
            var data = new List<object[]>
            {
                new object[] { "<table> <tbody> <tr> <td colspan=\"3\"> Decision Table Validator </td> </tr> </tbody> </table>", false, "Decision table must have at least three rows" }, // Table with only 1 name row
                new object[] { "<table> <tbody> <tr> <td colspan=\"3\"> Decision Table Name </td> </tr> <tr> <td> Decision Table Header 1 </td> <td> Decision Table Header 2 </td> <td> Decision Table Header 3 </td> </tr> </tbody> </table>", false, "Decision table must have at least three rows" }, // Table with 1 name and 1 header row
                new object[] { "<table> <tbody> <tr> <td> Decision Table Name </td> <td> </td> <td> </td> </tr> <tr> <td> Decision Table Header 1 </td> <td> Decision Table Header 2 </td> <td> Decision Table Header 3? </td> </tr> <tr> <td> Decision Table Data 1 </td> <td> Decision Table Data 2 </td> <td> Decision Table Data 3 </td> </tr> </tbody> </table>", false, "The first row of the decision table must have a single column" }, // Table name row has more than 1 column
                new object[] { "<table> <tbody> <tr> <td> Comment </td> </tr> <tr> <td> Decision Table Name </td> </tr> <tr> <td> Decision Table Header 1 </td> <td> Decision Table Header 2 </td> <td> Decision Table Header 3? </td> </tr> <tr> <td> Decision Table Data 1 </td> <td> Decision Table Data 2 </td> <td> Decision Table Data 3 </td> </tr> </tbody> </table>", false, "The first row is a comment row" }, // Table marked as comment
                new object[] { "<table> <tbody> <tr> <td colspan=\"3\"> Decision Table Name </td> </tr> <tr> <td> Decision Table Header 1 </td> <td> Decision Table Header 2 </td> <td> Decision Table Header 3? </td> </tr> <tr> <td> Decision Table Data 1 </td> <td> Decision Table Data 2 </td> <td> Decision Table Data 3 </td> </tr> </tbody> </table>", true, "" }, // Table with 1 name and 1 header and 1 data row
                new object[] { "<table> <thead> <tr> <th colspan=\"3\"> Decision Table Name </th> </tr> <tr> <th> Decision Table Header 1 </th> <th> Decision Table Header 2 </th> <th> Decision Table Header 3? </th> </tr> </thead> <tbody> <tr> <td> Decision Table Data 1 </td> <td> Decision Table Data 2 </td> <td> Decision Table Data 3 </td> </tr> </tbody> </table>", true, "" }, // Table with 1 name and 1 header row as headerand 1 datarow
            };

            return data;
        }

        private partial (bool, string) decision_table_validator_implementation(string decision_table);
    }

}