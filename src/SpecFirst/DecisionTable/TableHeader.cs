namespace SpecFirst.DecisionTable
{
    using System;

    public class TableHeader
    {
        private Type _dataType;

        public TableHeader(string name, TableHeaderType type)
        {
            Name = name;
            TableHeaderType = type;
        }

        public string Name { get; }
        public TableHeaderType TableHeaderType { get; }
        public Type DataType => _dataType;

        public void UpdateDataType(Type type)
        {
            _dataType = type;
        }
    }
}