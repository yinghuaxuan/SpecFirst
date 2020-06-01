namespace DecisionMarkd.DecisionTable
{
    using System;

    public class DecisionVariable
    {
        private Type _dataType;

        public DecisionVariable(string name, DecisionVariableType type)
        {
            Name = name;
            VariableType = type;
        }

        public string Name { get; }
        public DecisionVariableType VariableType { get; }
        public Type DataType => _dataType;

        public void UpdateDataType(Type type)
        {
            _dataType = type;
        }
    }
}