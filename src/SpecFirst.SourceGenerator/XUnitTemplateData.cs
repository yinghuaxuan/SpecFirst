namespace SpecFirst.TestsGenerator.xUnit
{
    using System.Collections.Generic;

    public class XUnitTemplateData
    {
        public string ClassName { get; set; }
        public string TestMethodParameters { get; set; }
        public string ImplMethodParameters { get; set; }
        public string ImplMethodArguments { get; set; }
        public string ImplMethodReturnValues { get; set; }
        public string ImplMethodReturnTypes { get; set; }
        public List<TestDataAndComment> TestDataAndComments { get; set; }
        public string AssertStatements { get; set; }
    }

    public class TestDataAndComment
    {
        public string TestData { get; set; }
        public string Comment { get; set; }
    }
}
