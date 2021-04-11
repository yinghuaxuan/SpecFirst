namespace SpecFirst.Core.Converter
{
    using System;
    using System.Collections.Generic;

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
                {typeof(double), "double"},
                {typeof(decimal), "decimal"},
                {typeof(bool), "bool"},
                {typeof(DateTime), "DateTime"},
            };
        }
    }
}
