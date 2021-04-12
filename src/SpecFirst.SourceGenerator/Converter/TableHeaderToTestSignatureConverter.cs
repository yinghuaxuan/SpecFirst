namespace SpecFirst.TestsGenerator.xUnit.Converter
{
    using System.Linq;
    using SpecFirst.Core.DecisionTable;
    using SpecFirst.TestsGenerator.xUnit.Test;

    public class TableHeaderToTestSignatureConverter
    {
        private readonly ITableHeaderToParameterConverter _parameterConverter;

        public TableHeaderToTestSignatureConverter(ITableHeaderToParameterConverter parameterConverter)
        {
            _parameterConverter = parameterConverter;
        }

        public UnitTest Convert(TableHeader[] tableHeaders)
        {
            var parameters = tableHeaders.Select(h => _parameterConverter.Convert(h));
            var test = new UnitTest(parameters);
            return test;
        }
    }
}
