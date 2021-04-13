namespace SpecFirst.TestsGenerator.xUnit.Test
{
    using System;
    using SpecFirst.Core.DecisionTable;

    public class Parameter
    {
        public string Type { get; set; }
        public string Name { get; set; }
        public TableHeaderType Direction { get; set; }
        public override string ToString()
        {
            return $"{Type} {Name}";
        }
    }
}