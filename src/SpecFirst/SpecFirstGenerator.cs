namespace SpecFirst
{
    using System;
    using Microsoft.CodeAnalysis;
    using SpecFirst.Setting;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using Microsoft.CodeAnalysis.Text;
    using SpecFirst.Core;
    using SpecFirst.Core.DecisionTable;

    [Generator]
    public sealed class SpecFirstGenerator : ISourceGenerator
    {
        private IDecisionTableMarkdownParser? _markdownParser;
        private ITestsGenerator? _testsGenerator;
        private SpecFirstSettingManager? _settingManager;

        public void Initialize(GeneratorInitializationContext context)
        {
            Debugger.Launch();
        }

        public void Execute(GeneratorExecutionContext context)
        {
            _settingManager = new SpecFirstSettingManager(context);
            _markdownParser = GetMarkdownParser(context);
            _testsGenerator = GetTestGenerator(context);

            IEnumerable<AdditionalText> markdownFiles =
                context.AdditionalFiles.Where(at => at.Path.EndsWith(_settingManager.Settings.SpecFileExtension));
            foreach (AdditionalText file in markdownFiles)
            {
                ProcessMarkdownFile(file, context);
            }
        }

        private ITestsGenerator GetTestGenerator(GeneratorExecutionContext context)
        {
            var allReferencedAssemblies = context.Compilation.References;
            foreach (var assembly in allReferencedAssemblies)
            {
                if (!assembly.Display.StartsWith(@"C:\Program Files"))
                {
                    try
                    {
                        var loadFrom = Assembly.LoadFrom(assembly.Display);
                        var type = loadFrom.GetTypes().FirstOrDefault(p => typeof(ITestsGenerator).IsAssignableFrom(p) && p.IsClass && !p.IsAbstract);
                        if (type != null)
                        {
                            return (ITestsGenerator)Activator.CreateInstance(type);
                        }
                    }
                    catch (Exception)
                    {
                    }
                }
            }

            return null;
        }

        private IDecisionTableMarkdownParser GetMarkdownParser(GeneratorExecutionContext context)
        {
            var allReferencedAssemblies = context.Compilation.References;
            foreach (var assembly in allReferencedAssemblies)
            {
                if (!assembly.Display.StartsWith(@"C:\Program Files"))
                {
                    try
                    {
                        var loadFrom = Assembly.LoadFrom(assembly.Display);
                        var type = loadFrom.GetTypes().FirstOrDefault(p => typeof(IDecisionTableMarkdownParser).IsAssignableFrom(p) && p.IsClass && !p.IsAbstract);
                        if (type != null)
                        {
                            return (IDecisionTableMarkdownParser)Activator.CreateInstance(type);
                        }
                    }
                    catch (Exception)
                    {
                    }
                }
            }

            return null;
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
            //context.AddSource($"{testFileName}", SourceText.From(sources[0], Encoding.UTF8));
            var testFile = Path.Combine(filePath, testFileName);
            File.WriteAllText(testFile, sources[0], Encoding.UTF8);
        }

        private void PersistTestImplFile(AdditionalText markdownFile, string filePath, string[] sources, GeneratorExecutionContext context)
        {
            string implementationFileName = _settingManager.GetTestImplFile(markdownFile);
            var implementationFile = Path.Combine(filePath, implementationFileName);
            if (!File.Exists(implementationFile))
            {
                //context.AddSource($"{implementationFileName}", SourceText.From(sources[1], Encoding.UTF8));
                File.WriteAllText(implementationFile, sources[1], Encoding.UTF8);
            }
        }
    }
}
