namespace Smeat.Leader.Core.Validation.BrokenRules
{
    /// <summary>
    /// Stores details about a specific broken business rule.
    /// </summary>
    public class BrokenRule : IBrokenRule
    {
        #region Variable Declarations

        private readonly string _ruleName;
        private readonly string _description;

        #endregion

        #region Constructors

        public BrokenRule(string ruleName, string description)
        {
            _ruleName = ruleName;
            _description = description;
        }

        #endregion

        #region Business Properties and Methods

        /// <summary>
        /// gets the name of the broken rule.
        /// </summary>
        /// <value>The name of the rule.</value>
        public string RuleName
        {
            get { return _ruleName; }
        }

        /// <summary>
        /// gets the description of the broken rule.
        /// </summary>
        /// <value>The description of the rule.</value>
        public string Description
        {
            get { return _description; }
        }

        #endregion
    }
}
