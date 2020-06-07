namespace DecisionMarkd
{
    using DecisionMarkd.DecisionTable.Parser;
    using DecisionMarkd.DecisionTable.Validator;
    using DecisionMarkd.Template.xUnit;
    using Markdig;
    using Microsoft.CodeAnalysis;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Linq;

    [Generator]
    public sealed class DecisionMarkdGenerator : ISourceGenerator
    {
        public void Initialize(InitializationContext context)
        {
            //Debugger.Launch();
        }

        public void Execute(SourceGeneratorContext context)
        {
            var settings = new DecisionMarkdSettingManager(context).Settings;
            IEnumerable<AdditionalText> markdownFiles =
                context.AdditionalFiles.Where(at => at.Path.EndsWith(settings.SpecFileExtension));
            foreach (AdditionalText file in markdownFiles)
            {
                ProcessMarkdownFile(file, context, settings);
            }
        }

        private void ProcessMarkdownFile(AdditionalText markdownFile, SourceGeneratorContext context, DecisionMarkdSettings settings)
        {
            string html = TryParseMarkdownToHtml(markdownFile, context);
            XDocument document = TryParseHtmlToXml(html);
            List<DecisionTable.DecisionTable> decisionTables = TryExtractDecisionTables(document);

            ITemplateGenerator templateGenerator = new XUnitTemplateGenerator();
            string[] sources = templateGenerator.Generate(settings.Namespace, decisionTables.ToArray());
            WriteTestFiles(markdownFile, context, settings, sources);
        }

        private static void WriteTestFiles(AdditionalText markdownFile, SourceGeneratorContext context, DecisionMarkdSettings settings, string[] sources)
        {
            string filePath = Path.GetDirectoryName(markdownFile.Path);
            if (!string.IsNullOrWhiteSpace(settings.TestFilePath))
            {
                string tempPath = Path.GetDirectoryName(markdownFile.Path);
                string[] paths = tempPath.Split(new[] { context.Compilation.AssemblyName }, StringSplitOptions.RemoveEmptyEntries);
                if (paths.Length == 2)
                {
                    filePath = settings.TestFilePath + paths[1];
                    Directory.CreateDirectory(filePath); // create the directory in case it doesn't already exist
                }
            }

            string specName = $"{new FileInfo(markdownFile.Path).Name.Replace(settings.SpecFileExtension, "")}";
            string testFileName = settings.TestFileNamePattern.Replace("{spec_name}", specName);
            //context.AddSource($"{testFileName}", SourceText.From(sources[0], Encoding.UTF8));
            File.WriteAllText(Path.Combine(filePath!, testFileName), sources[0], Encoding.UTF8);

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

        private static string TryParseMarkdownToHtml(AdditionalText markdownFile, SourceGeneratorContext context)
        {
            string markdownText = markdownFile.GetText(context.CancellationToken).ToString();
            string html;
            try
            {
                MarkdownPipeline pipeline = new MarkdownPipelineBuilder().UseAdvancedExtensions().Build();
                html = Markdown.ToHtml(markdownText, pipeline);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException($"Can not parse markdown file {markdownFile.Path} to HTML", e);
            }

            return html;
        }
    }
}
