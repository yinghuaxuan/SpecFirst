namespace SpecFirst
{
    using Microsoft.CodeAnalysis;
    using SpecFirst.Setting;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Text;
    using SpecFirst.Core;
    using SpecFirst.Core.DecisionTable;
    using SpecFirst.MarkdownParser;
    using SpecFirst.TestsGenerator.xUnit;

    [Generator]
    public sealed class SpecFirstGenerator : ISourceGenerator
    {
        private SpecFirstMarkdownParser _markdownParser;
        private ITestsGenerator _testsGenerator;
        private SpecFirstSettingManager _settingManager;

        public void Initialize(GeneratorInitializationContext context)
        {
            //Debugger.Launch();
            _markdownParser = new SpecFirstMarkdownParser();
            _testsGenerator = new XUnitTestsGenerator();
        }

        public void Execute(GeneratorExecutionContext context)
        {
            _settingManager = new SpecFirstSettingManager(context);
            IEnumerable<AdditionalText> markdownFiles =
                context.AdditionalFiles.Where(at => at.Path.EndsWith(_settingManager.Settings.SpecFileExtension));
            foreach (AdditionalText file in markdownFiles)
            {
                ProcessMarkdownFile(file, context);
            }
        }
         
        private void ProcessMarkdownFile(AdditionalText markdownFile, GeneratorExecutionContext context)
        {
            var markdownText = markdownFile.GetText(context.CancellationToken)?.ToString();
            List<DecisionTable> decisionTables = _markdownParser.Parse(markdownText);
            string[] sources = _testsGenerator.Generate(_settingManager.GetTestProject(), decisionTables.ToArray());
            PersistTestFiles(markdownFile, sources);
        }

        private void PersistTestFiles(AdditionalText markdownFile, string[] sources)
        {
            var filePath = _settingManager.GetTestFilePath(markdownFile);

            Directory.CreateDirectory(filePath!); // create the directory in case it doesn't already exist

            PersistTestFile(markdownFile, filePath, sources);

            PersistTestImplFile(markdownFile, filePath, sources);
        }

        private void PersistTestFile(AdditionalText markdownFile, string filePath, string[] sources)
        {
            string testFileName = _settingManager.GetTestFile(markdownFile);
            //context.AddSource($"{testFileName}", SourceText.From(sources[0], Encoding.UTF8));
            File.WriteAllText(Path.Combine(filePath!, testFileName), sources[0], Encoding.UTF8);
        }

        private void PersistTestImplFile(AdditionalText markdownFile, string filePath, string[] sources)
        {
            string implementationFileName = _settingManager.GetTestImplFile(markdownFile);
            if (!File.Exists(Path.Combine(filePath!, implementationFileName)))
            {
                //context.AddSource($"{implementationFileName}", SourceText.From(sources[1], Encoding.UTF8));
                File.WriteAllText(Path.Combine(filePath!, implementationFileName), sources[1], Encoding.UTF8);
            }
        }
    }
}
