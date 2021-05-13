namespace SpecFirst.Core.Setting
{
    public class SpecFirstSettings
    {
        public string SpecFileExtension { get; set; }
        public string TestingFramework { get; set; }
        public TestGeneration TestGeneration { get; set; }
    }

    public class TestGeneration
    {
        public string TestFilePath { get; set; }
        public string TestFile { get; set; }
        public string TestImplFile { get; set; }
    }
}