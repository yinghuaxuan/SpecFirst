namespace SpecFirst.TestsGenerator.xUnit.Test
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class UnitTest
    {
        private readonly List<Parameter> _inputParameters = new List<Parameter>();
        private readonly List<Parameter> _outParameters = new List<Parameter>();

        public UnitTest(IEnumerable<Parameter> parameters)
        {
            foreach (var parameter in parameters)
            {
                switch (parameter.Direction)
                {
                    case ParameterDirection.Comment:
                        break;
                    case ParameterDirection.Input:
                        _inputParameters.Add(parameter);
                        break;
                    case ParameterDirection.Output:
                        _outParameters.Add(parameter);
                        break;
                    default:
                        throw new InvalidOperationException();
                }
            }
        }


        public string TestMethodInputParameters => string.Join(", ", _inputParameters.Union(_outParameters));

        public string ImplMethodInputParameters => string.Join(", ", _inputParameters);
        public string ImplMethodInputArguments => string.Join(", ", _inputParameters.Select(p => p.Name));
        public string ImplMethodReturnValues => GetImplMethodReturnValues();
        public string ImplMethodReturnTypes => GetImplMethodReturnTypes();
        public IEnumerable<string> AssertStatements => _outParameters.Select(p => $"Assert.Equal({p.Name}_output, {p.Name})");

        private string GetImplMethodReturnValues()
        {
            string implMethodReturnValues;
            if (_outParameters.Count == 1) // string s1
            {
                implMethodReturnValues = string.Join(", ", _outParameters.Select(p => new Parameter{Type = p.Type, Name = $"{p.Name}_output"}));
            }
            else if (_outParameters.Count > 1) // (string s1, string s2)
            {
                implMethodReturnValues = $"({string.Join(", ", _outParameters.Select(p => new Parameter { Type = p.Type, Name = $"{p.Name}_output" }))})";
            }
            else // return ""
            {
                implMethodReturnValues = string.Empty;
            }

            return implMethodReturnValues;
        }

        private string GetImplMethodReturnTypes()
        {
            string implMethodReturnValues;
            if (_outParameters.Count == 1) // return parameter type only, e.g. string
            {
                implMethodReturnValues = string.Join(", ", _outParameters.Select(p => p.Type));
            }
            else if (_outParameters.Count > 1) // return type tuple, e.g. (string, string)
            {
                implMethodReturnValues = $"({string.Join(", ", _outParameters.Select(p => p.Type))})";
            }
            else // return "void"
            {
                implMethodReturnValues = "void";
            }

            return implMethodReturnValues;
        }
    }
}
