namespace SpecFirst.TestsGenerator.xUnit.Converter
{
    using System.Text.RegularExpressions;
    using SpecFirst.Core.DecisionTable;
    using SpecFirst.Core.Serialization;
    using SpecFirst.TestsGenerator.xUnit.Test;

    public class TableHeaderToParameterConverter : ITableHeaderToParameterConverter
    {
        private readonly INamingStrategy _namingStrategy;

        public TableHeaderToParameterConverter(INamingStrategy namingStrategy)
        {
            _namingStrategy = namingStrategy;
        }

        public Parameter Convert(TableHeader tableHeader)
        {
            var sanitizedName = ReplaceIllegalCharacters(tableHeader.Name);
            var parameterName = _namingStrategy.Resolve(sanitizedName);
            var parameterType = tableHeader.DataType;

            return new Parameter
            {
                Type = CSharpTypeAlias.Alias(parameterType),
                Name = parameterName,
                Direction = tableHeader.TableHeaderType
            };
        }

        private string ReplaceIllegalCharacters(string input)
        {
            return Regex.Replace(input, @"[^\w@-]", "_");
        }
    }
}
