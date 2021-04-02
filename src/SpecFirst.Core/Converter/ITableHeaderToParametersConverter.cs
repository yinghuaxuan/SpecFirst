namespace SpecFirst.Core.Converter
{
    using SpecFirst.Core.DecisionTable;

    public interface ITableHeaderToParametersConverter
    {
        string[] Convert(TableHeader tableHeader);
    }
}