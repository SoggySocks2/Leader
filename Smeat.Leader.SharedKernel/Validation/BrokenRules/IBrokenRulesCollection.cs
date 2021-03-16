using System.Collections.Generic;

namespace Smeat.Leader.SharedKernel.Validation.BrokenRules
{
    public interface IBrokenRulesCollection : IList<IBrokenRule>
    {
        /// <summary>
        /// Add a new broken rule.
        /// </summary>
        /// <param name="ruleName">Name of the broken rule.</param>
        /// <param name="description">Broken rule description.</param>
        /// <param name="isBroken">Is rule valid.</param>
        void AddRule(string ruleName, string description, bool isBroken);
    }
}
