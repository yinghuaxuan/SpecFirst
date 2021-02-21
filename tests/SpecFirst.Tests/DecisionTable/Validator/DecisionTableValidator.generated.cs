
namespace SpecFirst.Tests
{
    using System;
    using System.Collections.Generic;
    using Xunit;
    
    public partial class decision_table_validator
    {
        [Theory]
        [MemberData(nameof(get_test_data))]
        public void decision_table_validator_tests(String decision_table, Boolean is_valid, String validation_error)
        {
            decision_table_validator_implementation(decision_table, is_valid, validation_error);
        }

        public static IEnumerable<object[]> get_test_data()
        {
            var data = new List<object[]>
            {
                new object[] { "<table> <col style=\"width:72.34%\"/> <col style=\"width:10.64%\"/> <col style=\"width:17.02%\"/> <tbody> <tr> <td colspan=\"3\"> Decision Table Validator </td> </tr> </tbody> </table>", false, "Decision table must have at least three rows" },
                new object[] { "<table> <col style=\"width:72.34%\"/> <col style=\"width:10.64%\"/> <col style=\"width:17.02%\"/> <tbody> <tr> <td colspan=\"3\"> Decision Table Validator </td> </tr> <tr> <td> Decision Table </td> <td> Is Valid? </td> <td> Validation Error </td> </tr> </tbody> </table>", false, "Decision table must have at least three rows" },
                new object[] { "<table> <col style=\"width:72.34%\"/> <col style=\"width:10.64%\"/> <col style=\"width:17.02%\"/> <tbody> <tr> <td colspan=\"3\"> Decision Table Validator </td> </tr> <tr> <td> Decision Table </td> <td> Is Valid? </td> <td> Validation Error </td> </tr> <tr> <td> Decision Table </td> <td> Is Valid? </td> <td> Validation Error </td> </tr> </tbody> </table>", true, null },
                new object[] { "<table> <col style=\"width:72.34%\"/> <col style=\"width:10.64%\"/> <col style=\"width:17.02%\"/> <tbody> <tr> <td> Decision Table Validator </td> <td> </td> <td> </td> </tr> <tr> <td> Decision Table </td> <td> Is Valid? </td> <td> Validation Error </td> </tr> <tr> <td> Decision Table </td> <td> Is Valid? </td> <td> Validation Error </td> </tr> </tbody> </table>", false, "The first row of the decision table must have a single column" },
            };

            return data;
        }

        partial void decision_table_validator_implementation(String decision_table, Boolean is_valid, String validation_error);
    }

}