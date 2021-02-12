using System.Collections.Generic;

namespace Smeat.Leader.Core.Validation.BrokenRules
{
    public class BrokenRulesCollection : List<IBrokenRule>, IBrokenRulesCollection
    {
        #region Business Properties and Methods

        public void AddRule(string ruleName, string description, bool isBroken)
        {
            bool ruleExists = false;

            foreach (BrokenRule rule in this)
            {
                if (rule.RuleName == ruleName && rule.Description == description)
                {
                    //Rule already exists.
                    ruleExists = true;

                    if (isBroken == false)
                    {
                        //Rule is valid so remove it.
                        Remove(rule);
                        return;
                    }
                }
            }

            if (ruleExists == false && isBroken)
            {
                //Rule does not exist and is not valid, so add it.
                Add(new BrokenRule(ruleName, description));
            }
        }

        #endregion

        #region Constructors

        public BrokenRulesCollection()
        {

        }

        #endregion
    }
}
