﻿using DecisionMarkd.DecisionTable;
using DecisionMarkd.Template.Serialization;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace DecisionMarkd.Converter
{
    public class DecisionVariablesToParametersConverter
    {
        private SnakeCaseNamingStrategy _namingStrategy;

        public DecisionVariablesToParametersConverter()
        {
            _namingStrategy = new SnakeCaseNamingStrategy();
        }

        public string[] Convert(DecisionVariable[] decisionVariables, out int[] selectedDataIndices)
        {
            List<int> indices = new List<int>();
            StringBuilder testMethodParameterDeclarations = new StringBuilder();
            StringBuilder implementationMethodParameterDeclarations = new StringBuilder();
            StringBuilder implementationMethodParameters = new StringBuilder();
            for (int i = 0; i < decisionVariables.Length; i++)
            {
                if (decisionVariables[i].VariableType != DecisionVariableType.Comment)
                {
                    indices.Add(i);
                    var sanitizedName = ReplaceIllegalCharacters(decisionVariables[i].Name);
                    testMethodParameterDeclarations.Append(
                        $"{decisionVariables[i].DataType.Name} {_namingStrategy.Parse(sanitizedName)}, ");
                    implementationMethodParameterDeclarations.Append(
                        $"{decisionVariables[i].DataType.Name} {_namingStrategy.Parse(sanitizedName)}, ");
                    implementationMethodParameters.Append($"{_namingStrategy.Parse(sanitizedName)}, ");
                }
            }

            selectedDataIndices = indices.ToArray();

            return new[]
            {
                testMethodParameterDeclarations.Remove(testMethodParameterDeclarations.Length - 2, 2).ToString(),
                implementationMethodParameterDeclarations.Remove(implementationMethodParameterDeclarations.Length - 2, 2).ToString(),
                implementationMethodParameters.Remove(implementationMethodParameters.Length - 2, 2).ToString()
            };
        }

        private string ReplaceIllegalCharacters(string input)
        {
            return Regex.Replace(input, @"[^\w@-]", "_");
        }
    }

}
