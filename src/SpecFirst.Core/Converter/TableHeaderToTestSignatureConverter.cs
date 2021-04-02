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
            StringBuilder implMethodReturnValuesBuilder = new StringBuilder("void");
            
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
                            implMethodReturnValuesBuilder.Clear();
                        }
                        implMethodReturnValuesBuilder.Append($"{parameterType} {parameterName}, ");
                    }
                }
            }

            var testMethodParameters = testMethodParametersBuilder.Remove(testMethodParametersBuilder.Length - 2, 2).ToString();
            var implMethodParameters = implMethodParametersBuilder.Remove(implMethodParametersBuilder.Length - 2, 2).ToString();
            var implMethodArguments = implMethodArgumentsBuilder.Remove(implMethodArgumentsBuilder.Length - 2, 2).ToString();
            var implMethodReturnValues = GetImplMethodReturnValues(numberOfReturnValues, implMethodReturnValuesBuilder);

            return new[]
            {
                testMethodParameters,
                implMethodParameters,
                implMethodArguments,
                implMethodReturnValues
            };
        }

        private static string GetImplMethodReturnValues(int numberOfReturnValues, StringBuilder implMethodReturnValuesBuilder)
        {
            string implMethodReturnValues;
            if (numberOfReturnValues == 1) // return parameter type only, e.g. string
            {
                implMethodReturnValues = implMethodReturnValuesBuilder.Remove(implMethodReturnValuesBuilder.Length - 2, 2).ToString();
                implMethodReturnValues = implMethodReturnValues.Split(' ')[0];
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
