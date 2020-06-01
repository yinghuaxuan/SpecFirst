namespace DecisionMarkd
{
    using DecisionMarkd.DecisionTable.Parser;
    using DecisionMarkd.DecisionTable.Validator;
    using DecisionMarkd.Template.xUnit;
    using HandlebarsDotNet;
    using Markdig;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.Text;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Linq;

    [Generator]
    public sealed class DecisionMarkGenerator : ISourceGenerator
    {
        public void Initialize(InitializationContext context)
        {
            Debugger.Launch();
        }

        public void Execute(SourceGeneratorContext context)
        {
            IEnumerable<AdditionalText> markdownFiles =
                context.AdditionalFiles.Where(at => at.Path.EndsWith(".spec.md"));
            foreach (AdditionalText file in markdownFiles)
            {
                ProcessMarkdownFile(file, context);
            }
        }

        private void ProcessMarkdownFile(AdditionalText markdownFile, SourceGeneratorContext context)
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
                throw new InvalidOperationException($"Can not parse markdown file {markdownFile.Path}", e);
            }

            List<DecisionTable.DecisionTable> decisionTables = new List<DecisionTable.DecisionTable>();
            XDocument document;
            try
            {
                document = XDocument.Parse("<html>" + html + "</html>");
            }
            catch (Exception e)
            {
                throw new InvalidOperationException($"Can not parse the generated html text into xml document {markdownFile.Path}", e);
            }

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

            ITemplateGenerator templateGenerator = new XUnitTemplateGenerator();
            string[] sources = templateGenerator.Generate(context.Compilation.AssemblyName, decisionTables.ToArray());

            string filePath = Path.GetDirectoryName(markdownFile.Path);

            string testFileName = $"{new FileInfo(markdownFile.Path).Name.Replace(".spec.md", "")}.generated.cs";
            context.AddSource($"{testFileName}", SourceText.From(sources[0], Encoding.UTF8));
            File.WriteAllText(Path.Combine(filePath!, testFileName), sources[0], Encoding.UTF8);

            string implementationFileName = $"{new FileInfo(markdownFile.Path).Name.Replace(".spec.md", "")}.implementation.cs";
            context.AddSource($"{implementationFileName}", SourceText.From(sources[1], Encoding.UTF8));
            File.WriteAllText(Path.Combine(filePath!, implementationFileName), sources[1], Encoding.UTF8);
        }
    }
}
