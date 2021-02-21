namespace SpecFirst
{
    using SpecFirst.DecisionTable.Parser;
    using SpecFirst.DecisionTable.Validator;
    using DecisionMarkd.Template.xUnit;
    using Markdig;
    using Microsoft.CodeAnalysis;
    using SpecFirst.Setting;
    using SpecFirst.Template.xUnit;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Linq;

    [Generator]
    public sealed class SpecFirstGenerator : ISourceGenerator
    {
        public void Initialize(GeneratorInitializationContext context)
        {
            Debugger.Launch();
        }

        public void Execute(GeneratorExecutionContext context)
        {
            var settings = new SpecFirstSettingManager(context).Settings;
            IEnumerable<AdditionalText> markdownFiles =
                context.AdditionalFiles.Where(at => at.Path.EndsWith(settings.SpecFileExtension));
            foreach (AdditionalText file in markdownFiles)
            {
                ProcessMarkdownFile(file, context, settings);
            }
        }
         
        private void ProcessMarkdownFile(AdditionalText markdownFile, GeneratorExecutionContext context, SpecFirstSettings settings)
        {
            string html = TryParseMarkdownToHtml(markdownFile, context);
            XDocument document = TryParseHtmlToXml(html);
            List<DecisionTable.DecisionTable> decisionTables = TryExtractDecisionTables(document);

            ITestSourceGenerator testSourceGenerator = new XUnitSourceGenerator();
            string[] sources = testSourceGenerator.Generate(settings.Namespace, decisionTables.ToArray());
            PersistTestFiles(markdownFile, context, settings, sources);
        }

        private static void PersistTestFiles(AdditionalText markdownFile, GeneratorExecutionContext context, SpecFirstSettings settings, string[] sources)
        {
            var filePath = GetFilePath(markdownFile, context, settings);

            Directory.CreateDirectory(filePath!); // create the directory in case it doesn't already exist

            var testFile = PersistTestFile(markdownFile, settings, filePath, sources);

            PersistTestImplementationFile(settings, testFile, filePath, sources);
        }

        private static string GetFilePath(AdditionalText additionalText, GeneratorExecutionContext generatorExecutionContext, SpecFirstSettings specFirstSettings)
        {
            string filePath = Path.GetDirectoryName(additionalText.Path);
            if (!string.IsNullOrWhiteSpace(specFirstSettings.TestFilePath))
            {
                string tempPath = Path.GetDirectoryName(additionalText.Path);
                string[] paths = tempPath!.Split(new[] {generatorExecutionContext.Compilation.AssemblyName}, StringSplitOptions.RemoveEmptyEntries);
                if (paths.Length == 2)
                {
                    filePath = specFirstSettings.TestFilePath + paths[1];
                }
            }

            return filePath;
        }

        private static string PersistTestFile(AdditionalText markdownFile, SpecFirstSettings settings, string filePath, string[] sources)
        {
            string specName = $"{new FileInfo(markdownFile.Path).Name.Replace(settings.SpecFileExtension, "")}";
            string testFileName = settings.TestFileNamePattern.Replace("{spec_name}", specName);
            //context.AddSource($"{testFileName}", SourceText.From(sources[0], Encoding.UTF8));
            File.WriteAllText(Path.Combine(filePath!, testFileName), sources[0], Encoding.UTF8);
            return specName;
        }

        private static void PersistTestImplementationFile(SpecFirstSettings settings, string specName, string filePath, string[] sources)
        {
            string implementationFileName = settings.ImplementationFileNamePattern.Replace("{spec_name}", specName);
            if (!File.Exists(Path.Combine(filePath!, implementationFileName)))
            {
                //context.AddSource($"{implementationFileName}", SourceText.From(sources[1], Encoding.UTF8));
                File.WriteAllText(Path.Combine(filePath!, implementationFileName), sources[1], Encoding.UTF8);
            }
        }

        private static List<DecisionTable.DecisionTable> TryExtractDecisionTables(XDocument document)
        {
            List<DecisionTable.DecisionTable> decisionTables = new List<DecisionTable.DecisionTable>();
            IEnumerable<XElement> tables = document.Descendants("table");
            foreach (XElement table in tables)
            {
                if (new DecisionTableHtmlValidator().Validate(table).IsValid)
                {
                    try
                    {
                        DecisionTable.DecisionTable decisionTable = new DecisionTableParser().Parse(table);
                        decisionTables.Add(decisionTable);
                    }
                    catch (Exception e)
                    {
                        // do nothing
                    }
                }
            }

            return decisionTables;
        }

        private static XDocument TryParseHtmlToXml(string html)
        {
            XDocument document;
            try
            {
                document = XDocument.Parse("<html>" + html + "</html>");
            }
            catch (Exception e)
            {
                throw new InvalidOperationException($"Can not parse the generated html into xml", e);
            }

            return document;
        }

        private static string TryParseMarkdownToHtml(AdditionalText markdownFile, GeneratorExecutionContext context)
        {
            string markdownText = markdownFile.GetText(context.CancellationToken).ToString();
            string html = null;
            try
            {
                var engine = new Jurassic.ScriptEngine();
                engine.ExecuteFile(@"D:\DEV\Projects\spec-first\src\SpecFirst\Script\test.js");
                //MarkdownPipeline pipeline = new MarkdownPipelineBuilder().UseAdvancedExtensions().Build();
                //html = Markdown.ToHtml(markdownText, pipeline);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException($"Can not parse markdown file {markdownFile.Path} to HTML", e);
            }

            return html;
        }
    }
}
