namespace MarkedDecision
{
    using System.Collections.Generic;
    using System.Linq;

    public class ValidationResult
    {
        public ValidationResult(IList<string> error)
        {
            Error = error;
        }

        public bool IsValid => !Error?.Any() ?? true;
        public IList<string> Error { get; }
    }
}