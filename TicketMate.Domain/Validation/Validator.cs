namespace TicketMate.Domain.Validation
{
    public class Validator
    {
        public Validator()
        {

        }

        public Validator(params IValidationRule[] rules)
        {
            ApplyRules(rules);
        }

        private readonly List<string> _validationFailureReasons = new List<string>();

        public List<string> ReasonsForFailure => _validationFailureReasons;

        public bool IsPassingAllRules => _validationFailureReasons.Count == 0;

        public void ApplyRule(IValidationRule validationRule)
        {
            if (!validationRule.IsPassingRule(out var validationFailureMessage))
            {
                _validationFailureReasons.Add(validationFailureMessage);
            }
        }

        public void ApplyRules(params IValidationRule[] rules)
        {
            foreach (var rule in rules)
            {
                ApplyRule(rule);
            }
        }
    }
}
