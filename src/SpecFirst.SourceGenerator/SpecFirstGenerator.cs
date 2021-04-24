namespace SpecFirst
{
    using Microsoft.CodeAnalysis;
    using SpecFirst.Setting;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Text;
    using Microsoft.CodeAnalysis.Text;
    using SpecFirst.Core;
    using SpecFirst.Core.DecisionTable;
    using SpecFirst.MarkdownParser;
    using SpecFirst.TestsGenerator.xUnit;

    [Generator]
    public sealed class SpecFirstGenerator : ISourceGenerator
    {
        private SpecFirstDecisionTableMarkdownParser _markdownParser;
        private ITestsGenerator _testsGenerator;
        private SpecFirstSettingManager _settingManager;

        public void Initialize(GeneratorInitializationContext context)
        {
            Debugger.Launch();
            _markdownParser = new SpecFirstDecisionTableMarkdownParser();
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
            PersistTestFiles(markdownFile, sources, context);
        }

        private void PersistTestFiles(AdditionalText markdownFile, string[] sources, GeneratorExecutionContext context)
        {
            var filePath = _settingManager.GetTestFilePath(markdownFile);

            Directory.CreateDirectory(filePath); // create the directory in case it doesn't exist

            PersistTestFile(markdownFile, filePath, sources, context);

            PersistTestImplFile(markdownFile, filePath, sources, context);
        }

        private void PersistTestFile(AdditionalText markdownFile, string filePath, string[] sources, GeneratorExecutionContext context)
        {
            string testFileName = _settingManager.GetTestFile(markdownFile);
            context.AddSource($"{testFileName}", SourceText.From(sources[0], Encoding.UTF8));
            File.WriteAllText(Path.Combine(filePath, testFileName), sources[0], Encoding.UTF8);
        }

        private void PersistTestImplFile(AdditionalText markdownFile, string filePath, string[] sources, GeneratorExecutionContext context)
        {
            string implementationFileName = _settingManager.GetTestImplFile(markdownFile);
            context.AddSource($"{implementationFileName}", SourceText.From(sources[1], Encoding.UTF8));
            if (!File.Exists(Path.Combine(filePath, implementationFileName)))
            {
                File.WriteAllText(Path.Combine(filePath, implementationFileName), sources[1], Encoding.UTF8);
            }
        }
    }
}
