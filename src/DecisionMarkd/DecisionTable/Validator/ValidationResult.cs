namespace DecisionMarkd.DecisionTable.Validator
{
    public class ValidationResult
    {
        public ValidationResult(string error)
        {
            Error = error;
        }

        public bool IsValid => string.IsNullOrEmpty(Error);
        public string Error { get; }
    }
}