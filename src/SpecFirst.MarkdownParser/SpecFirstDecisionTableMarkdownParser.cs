
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

    public sealed class SpecFirstDecisionTableMarkdownParser : IDecisionTableMarkdownParser
    {
        private readonly IDecisionTableParser _tableParser;
        private readonly IDecisionTableHtmlValidator _tableValidator;

        public SpecFirstDecisionTableMarkdownParser()
        {
            _tableParser = new DecisionTableParser();
            _tableValidator = new DecisionTableHtmlValidator();
        }

        public List<DecisionTable> Parse(string markdownText)
        {
            string html = TryParseMarkdownToHtml(markdownText);
            html = html.Replace("<br>", "<br/>");
            XDocument document = TryParseHtmlToXml(html);
            List<DecisionTable> decisionTables = TryExtractDecisionTables(document);
            return decisionTables;
        }

        private List<DecisionTable> TryExtractDecisionTables(XDocument document)
        {
            List<DecisionTable> decisionTables = new List<DecisionTable>();
            IEnumerable<XElement> tables = document.Descendants("table");
            foreach (XElement table in tables)
            {
                if (_tableValidator.Validate(table, out _))
                {
                    try
                    {
                        DecisionTable decisionTable = _tableParser.Parse(table);
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

        private static string TryParseMarkdownToHtml(string markdownText)
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

        public static string GetScriptFile()
        {
            var assembly = Assembly.GetExecutingAssembly().Location;
            var directory = Path.GetDirectoryName(assembly);
            return Path.Combine(directory, "Script\\bundle.js");
        }
    }
}
