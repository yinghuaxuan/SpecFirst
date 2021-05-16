namespace SpecFirst.Core.Setting
{
    public sealed class SpecFirstSettings
    {
        public const string DefaultTestingFramework = "xUnit";
        public const string DefaultTestProject = "{spec_project}.Tests";
        public const string DefaultTestFileName = "{spec_name}Tests.g.cs";
        public const string DefaultImplementationFileName = "{spec_name}Tests.impl.g.cs";
        public const string DefaultSpecFileExtension = ".spec.md";

        public string SpecFileExtension { get; set; }
        public string TestingFramework { get; set; }
        public TestGeneration TestGeneration { get; set; }
    }

    public class TestGeneration
    {
        public string TestProject { get; set; }
        public string TestFileName { get; set; }
        public string TestImplFileName { get; set; }
    }
}