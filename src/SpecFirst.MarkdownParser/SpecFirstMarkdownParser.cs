
namespace SpecFirst.MarkdownParser
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Linq;
    using Jurassic;
    using SpecFirst.Core;
    using SpecFirst.Core.DecisionTable;
    using SpecFirst.Core.DecisionTable.Parser;
    using SpecFirst.Core.DecisionTable.Validator;

    public class SpecFirstMarkdownParser : IMarkdownParser
    {
        public List<DecisionTable> Parse(string markdownText)
        {
            string html = TryParseMarkdownToHtml(markdownText);
            XDocument document = TryParseHtmlToXml(html);
            List<DecisionTable> decisionTables = TryExtractDecisionTables(document);
            return decisionTables;
        }

        private static List<DecisionTable> TryExtractDecisionTables(XDocument document)
        {
            List<DecisionTable> decisionTables = new List<DecisionTable>();
            IEnumerable<XElement> tables = document.Descendants("table");
            foreach (XElement table in tables)
            {
                if (new DecisionTableHtmlValidator().Validate(table).IsValid)
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
                engine.ExecuteFile("bundle.js");
                html = engine.GetGlobalValue("result").ToString();
            }
            catch (Exception e)
            {
                throw new InvalidOperationException($"Can not parse markdown text to HTML", e);
            }

            return html;
        }
    }
}
