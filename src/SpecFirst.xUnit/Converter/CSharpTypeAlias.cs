namespace SpecFirst.xUnit.Converter
{
    using System;
    using System.Collections.Generic;
    using SpecFirst.Core.TypeResolver;

    public static class CSharpTypeAlias
    {
        private static readonly Dictionary<Type, string> _alias;

        static CSharpTypeAlias()
        {
            _alias = SetupAlias();
        }

        public static string Alias(Type type)
        {
            return _alias[type];
        }

        private static Dictionary<Type, string> SetupAlias()
        {
            return new Dictionary<Type, string>
            {
                {typeof(object), "object"},
                {typeof(string), "string"},
                {typeof(int), "int"},
                {typeof(IntType), "int"},
                {typeof(double), "double"},
                {typeof(DoubleType), "double"},
                {typeof(decimal), "decimal"},
                {typeof(DecimalType), "decimal"},
                {typeof(bool), "bool"},
                {typeof(DateTime), "DateTime"},
            };
        }
    }
}
