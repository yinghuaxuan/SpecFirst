namespace SpecFirst.xUnit.Converter
{
    using System.Linq;
    using SpecFirst.Core.DecisionTable;
    using SpecFirst.xUnit.Test;

    public class TableHeaderToTestSignatureConverter
    {
        private readonly ITableHeaderToParameterConverter _parameterConverter;

        public TableHeaderToTestSignatureConverter(ITableHeaderToParameterConverter parameterConverter)
        {
            _parameterConverter = parameterConverter;
        }

        public TestGeneration Convert(TableHeader[] tableHeaders)
        {
            var parameters = tableHeaders.Select(h => _parameterConverter.Convert(h));
            var test = new TestGeneration(parameters);
            return test;
        }
    }
}
