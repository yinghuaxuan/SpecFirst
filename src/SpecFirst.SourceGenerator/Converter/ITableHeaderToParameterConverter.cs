namespace SpecFirst.TestsGenerator.xUnit.Converter
{
    using SpecFirst.Core.DecisionTable;
    using SpecFirst.TestsGenerator.xUnit.Test;

    public interface ITableHeaderToParameterConverter
    {
        Parameter Convert(TableHeader tableHeader);
    }
}