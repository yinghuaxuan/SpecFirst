namespace SpecFirst.Core.DecisionTable.Validator
{
    using System.Collections.Generic;
    using System.Xml.Linq;

    public interface IDecisionTableHtmlValidator
    {
        /// <summary>
        /// Validate a table as decision table.
        /// </summary>
        /// <param name="document">The XElement representing a html table.</param>
        /// <param name="errors">The validation errors if any.</param>
        /// <returns>Return true if a table is decision table.</returns>
        bool Validate(XElement document, out IEnumerable<string> errors);
    }
}