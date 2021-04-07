namespace SpecFirst.TestsGenerator.xUnit
{
    using System;
    using System.Collections.Generic;

    public class UnitTest
    {
        public TestMethod TestMethod { get; set; }
        public TestMethod ImplementationMethod { get; set; }
    }

    public class TestMethod
    {
        public string MethodName { get; set; }
        public string MethodReturnType { get; set; }
        public List<TypeNamePair> Parameters { get; set; }
    }


    public class TypeNamePair
    {
        public TypeNamePair(Type type, string name) : this (type, name, null)
        {
        }

        public TypeNamePair(Type type, string name, string direction)
        {
            Type = type;
            Name = name;
            Direction = direction;
        }

        public Type Type { get; set; }
        public string Name { get; set; }
        public string Direction { get; set; }
    }
}
