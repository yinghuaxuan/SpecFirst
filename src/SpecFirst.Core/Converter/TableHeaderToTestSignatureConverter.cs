namespace SpecFirst.Core.Converter
{
    using System.Text;
    using SpecFirst.Core.DecisionTable;

    public class TableHeaderToTestSignatureConverter
    {
        private readonly ITableHeaderToParametersConverter _parametersConverter;

        public TableHeaderToTestSignatureConverter(ITableHeaderToParametersConverter parametersConverter)
        {
            _parametersConverter = parametersConverter;
        }

        public string[] Convert(TableHeader[] tableHeaders)
        {
            StringBuilder testMethodParametersBuilder = new StringBuilder();
            StringBuilder implMethodParametersBuilder = new StringBuilder();
            StringBuilder implMethodArgumentsBuilder = new StringBuilder();
            StringBuilder implMethodReturnTypesBuilder = new StringBuilder("void");
            StringBuilder implMethodReturnValuesBuilder = new StringBuilder();
            StringBuilder assertStatementsBuilder = new StringBuilder();

            int numberOfReturnValues = 0;
            for (int i = 0; i < tableHeaders.Length; i++)
            {
                if (tableHeaders[i].TableHeaderType != TableHeaderType.Comment)
                {
                    var parameter = _parametersConverter.Convert(tableHeaders[i]);
                    var parameterType = parameter[0];
                    var parameterName = parameter[1];
                    testMethodParametersBuilder.Append($"{parameterType} {parameterName}, ");

                    if (tableHeaders[i].TableHeaderType == TableHeaderType.Input)
                    {
                        implMethodParametersBuilder.Append($"{parameterType} {parameterName}, ");
                        implMethodArgumentsBuilder.Append($"{parameterName}, ");
                    }
                    else
                    {
                        numberOfReturnValues++;
                        if (numberOfReturnValues == 1)
                        {
                            implMethodReturnTypesBuilder.Clear();
                        }
                        implMethodReturnValuesBuilder.Append($"{parameterType} {parameterName}_output, ");
                        implMethodReturnTypesBuilder.Append($"{parameterType}, ");
                        assertStatementsBuilder.AppendLine($"Assert.Equal({parameterName}_output, {parameterName});");
                    }
                }
            }

            var testMethodParameters = testMethodParametersBuilder.Remove(testMethodParametersBuilder.Length - 2, 2).ToString();
            var implMethodParameters = implMethodParametersBuilder.Remove(implMethodParametersBuilder.Length - 2, 2).ToString();
            var implMethodArguments = implMethodArgumentsBuilder.Remove(implMethodArgumentsBuilder.Length - 2, 2).ToString();
            var implMethodReturnTypes = GetImplMethodReturnTypes(numberOfReturnValues, implMethodReturnTypesBuilder);
            var implMethodReturnValues = GetImplMethodReturnValues(numberOfReturnValues, implMethodReturnValuesBuilder);
            var assertStatements = assertStatementsBuilder.ToString();

            return new[]
            {
                testMethodParameters,
                implMethodParameters,
                implMethodArguments,
                implMethodReturnTypes,
                implMethodReturnValues,
                assertStatements
            };
        }

        private static string GetImplMethodReturnTypes(int numberOfReturnValues, StringBuilder implMethodReturnTypesBuilder)
        {
            string implMethodReturnValues;
            if (numberOfReturnValues == 1) // return parameter type only, e.g. string
            {
                implMethodReturnValues = implMethodReturnTypesBuilder.Remove(implMethodReturnTypesBuilder.Length - 2, 2).ToString();
            }
            else if (numberOfReturnValues > 1) // return tuple, e.g. (string s1, string s2)
            {
                implMethodReturnValues = implMethodReturnTypesBuilder.Remove(implMethodReturnTypesBuilder.Length - 2, 2).Append(")").Insert(0, "(").ToString();
            }
            else // return "void"
            {
                implMethodReturnValues = implMethodReturnTypesBuilder.ToString();
            }

            return implMethodReturnValues;
        }

        private static string GetImplMethodReturnValues(int numberOfReturnValues, StringBuilder implMethodReturnValuesBuilder)
        {
            string implMethodReturnValues;
            if (numberOfReturnValues == 1) // return parameter type only, e.g. string
            {
                implMethodReturnValues = implMethodReturnValuesBuilder.Remove(implMethodReturnValuesBuilder.Length - 2, 2).ToString();
            }
            else if (numberOfReturnValues > 1) // return tuple, e.g. (string s1, string s2)
            {
                implMethodReturnValues = implMethodReturnValuesBuilder.Remove(implMethodReturnValuesBuilder.Length - 2, 2).Append(")").Insert(0, "(").ToString();
            }
            else // return "void"
            {
                implMethodReturnValues = implMethodReturnValuesBuilder.ToString();
            }

            return implMethodReturnValues;
        }
    }
}
