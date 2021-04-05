namespace SpecFirst.Core.TypeResolver
{
    using System;
    using System.Collections.Generic;

    public class TypeDecisionNode
    {
        public TypeDecisionNode()
        {
            ChildNodes = new List<TypeDecisionNode>();
        }

        public Func<char, bool> ShouldProcess { get; set; }
        public List<TypeDecisionNode> ChildNodes { get; set; }
        public Func<string, TypeValuePair> NodeType { get; set; }
    }
}