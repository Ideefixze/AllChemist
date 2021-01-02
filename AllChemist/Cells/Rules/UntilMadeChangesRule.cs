using AllChemist.Model;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace AllChemist.Cells.Rules
{
    /// <summary>
    /// Composite rule: execute all the rules until some number of childrenRules already have been executed.
    /// </summary>

    [JsonObject]
    public class UntilMadeChangesRule : CompositeRule
    {
        [JsonProperty]
        private int rulesMadeAnyChangesCountToEnd;

        public UntilMadeChangesRule(List<IRule> rules, int count)
        {
            childrenRules = rules;
            rulesMadeAnyChangesCountToEnd = count;
        }

        //Json constructor because constructor above somehow doesn't like Newtonsoft's Json 
        public UntilMadeChangesRule()
        {

        }

        public override bool ExecuteRule(World world, Cell cell)
        {
            int k = rulesMadeAnyChangesCountToEnd;
            bool anyTrue = false;
            foreach (IRule rule in childrenRules)
            {
                if (rule.ExecuteRule(world, cell))
                {
                    k--;
                    anyTrue = true;
                }

                if (k == 0)
                    break;
            }
            return anyTrue;
        }

        public override string ToString()
        {
            string s = $"UntilMadeChangesRule {rulesMadeAnyChangesCountToEnd} \n" + base.ToString();
            return s;
        }
    }
}