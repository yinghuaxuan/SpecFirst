namespace SpecFirst.xUnit.Converter
{
    using SpecFirst.Core.DecisionTable;
    using SpecFirst.xUnit.Test;

    public interface ITableHeaderToParameterConverter
    {
        Parameter Convert(TableHeader tableHeader);
    }
}