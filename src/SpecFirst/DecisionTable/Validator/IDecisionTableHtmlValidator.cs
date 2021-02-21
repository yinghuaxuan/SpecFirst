namespace SpecFirst.DecisionTable.Validator
{
    using System.Xml.Linq;

    public interface IDecisionTableHtmlValidator
    {
        /// <summary>
        /// Validate a table as decision table.
        /// </summary>
        /// <param name="document">The XElement representing a html table.</param>
        /// <returns>Return true if a table is decision table.</returns>
        ValidationResult Validate(XElement document);
    }
}