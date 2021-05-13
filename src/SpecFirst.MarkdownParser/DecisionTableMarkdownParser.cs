
namespace SpecFirst.MarkdownParser
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;
    using System.Xml.Linq;
    using Jurassic;
    using SpecFirst.Core;
    using SpecFirst.Core.DecisionTable;
    using SpecFirst.Core.DecisionTable.Parser;
    using SpecFirst.Core.DecisionTable.Validator;

    public sealed class DecisionTableMarkdownParser : IDecisionTableMarkdownParser
    {
        private readonly IDecisionTableParser _tableParser;
        private readonly IDecisionTableHtmlValidator _tableValidator;

        public DecisionTableMarkdownParser()
        {
            _tableParser = new DecisionTableParser();
            _tableValidator = new DecisionTableHtmlValidator();
        }

        public IEnumerable<DecisionTable> Parse(string markdownText)
        {
            string html = ParseMarkdownToHtml(markdownText);
            html = html.Replace("<br>", "<br/>");
            XDocument document = ParseHtmlToXml(html);
            List<DecisionTable> decisionTables = ExtractDecisionTables(document);
            return decisionTables;
        }

        private List<DecisionTable> ExtractDecisionTables(XDocument document)
        {
            List<DecisionTable> decisionTables = new List<DecisionTable>();
            IEnumerable<XElement> tables = document.Descendants("table");
            foreach (XElement table in tables)
            {
                if (_tableValidator.Validate(table, out _))
                {
                    DecisionTable decisionTable = _tableParser.Parse(table);
                    decisionTables.Add(decisionTable);
                }
            }

            return decisionTables;
        }

        private static XDocument ParseHtmlToXml(string html)
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

        private static string ParseMarkdownToHtml(string markdownText)
        {
            string html;
            try
            {
                var engine = new ScriptEngine();
                engine.SetGlobalValue("markdownTable", markdownText);
                engine.ExecuteFile(GetScriptFile());
                html = engine.GetGlobalValue("result").ToString();
            }
            catch (Exception e)
            {
                throw new InvalidOperationException($"Can not parse markdown text to HTML", e);
            }

            return html;
        }

        private static string GetScriptFile()
        {
            var assemblyLocation = Assembly.GetExecutingAssembly().Location;
            var directory = Path.GetDirectoryName(assemblyLocation);
            return Path.Combine(directory!, "Script\\bundle.js");
        }
    }
}
