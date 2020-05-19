namespace DecisionMarkd
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Linq;
    using HandlebarsDotNet;
    using Markdig;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.Text;

    [Generator]
    public sealed class DecisionMarkGenerator : ISourceGenerator
    {
        private const string XunitTemplate = @"
namespace {{namespace_name}}
{
    using System.Collections.Generic;
    using Xunit;

    public partial class {{class_name}}
    {
        [Theory]
        [MemberData(nameof(get_test_data))]
        public void {{class_name}}_tests({{parameter_declarations}})
        {
            {{class_name}}_implementation({{parameters}});
        }

        public static IEnumerable<object[]> get_test_data()
        {
            var data = new List<object[]>
            {
                {{#each list_of_test_data}}
                new object[] { {{this}} },
                {{/each}}
            };

            return data;
        }

        partial void {{class_name}}_implementation({{parameter_declarations}});
    }
}";

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

            List<DecisionTable> decisionTables = new List<DecisionTable>();
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
                if (new DecisionTableValidator().Validate(table).IsValid)
                {
                    try
                    {
                        DecisionTable decisionTable = new DecisionTableParser().Parse(table);
                        decisionTables.Add(decisionTable);
                    }
                    catch (Exception e)
                    {
                        // do nothing
                    }
                }
            }

            Func<object, string> compiled = Handlebars.Compile(XunitTemplate);
            StringBuilder classSources = new StringBuilder();
            foreach (DecisionTable table in decisionTables)
            {
                string classSource = compiled(new
                {
                    namespace_name = context.Compilation.GlobalNamespace.Name,
                    class_name = table.ClassName,
                    parameter_declarations = table.Parameters,
                    parameters = table.Parameters.Replace("object ", ""),
                    list_of_test_data = table.TestData,
                });

                classSources.AppendLine(classSource);
            }

            string filePath = Path.GetDirectoryName(markdownFile.Path);
            string fileName = $"{new FileInfo(markdownFile.Path).Name.Replace(".spec.md", "")}.generated.cs";
            context.AddSource($"{fileName}", SourceText.From(classSources.ToString(), Encoding.UTF8));
            File.WriteAllText(Path.Combine(filePath!, fileName), classSources.ToString());
        }
    }
}
