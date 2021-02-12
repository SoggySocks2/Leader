namespace Smeat.Leader.Core.Validation.BrokenRules
{
    public interface IBrokenRule
    {
        /// <summary>
        /// gets the name of the broken rule.
        /// </summary>
        /// <value>The name of the rule.</value>
        string RuleName { get; }

        /// <summary>
        /// gets the description of the broken rule.
        /// </summary>
        /// <value>The description of the rule.</value>
        string Description { get;  }
    }
}
