namespace SpecFirst.Core.Converter
{
    using System.Text.RegularExpressions;
    using SpecFirst.Core.DecisionTable;
    using SpecFirst.Core.Serialization;

    public class TableHeaderToParametersConverter : ITableHeaderToParametersConverter
    {
        private readonly INamingStrategy _namingStrategy;

        public TableHeaderToParametersConverter(INamingStrategy namingStrategy)
        {
            _namingStrategy = namingStrategy;
        }

        public string[] Convert(TableHeader tableHeader)
        {
            var sanitizedName = ReplaceIllegalCharacters(tableHeader.Name);
            var parameterName = _namingStrategy.Resolve(sanitizedName);
            var parameterType = tableHeader.DataType.Name;

            return new[]
            {
                parameterType,
                parameterName
            };
        }

        private string ReplaceIllegalCharacters(string input)
        {
            return Regex.Replace(input, @"[^\w@-]", "_");
        }
    }
}
