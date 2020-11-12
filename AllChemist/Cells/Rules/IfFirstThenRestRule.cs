using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using AllChemist.Cells.Rules;
using AllChemist.Model;
using Newtonsoft.Json;

namespace AllChemist.Cells.Rules
{
    /// <summary>
    /// Composite Rule. If first rule from list has returned true: execute all the other rules.
    /// </summary>

    [JsonObject]
    public class IfFirstThenRestRule : CompositeRule
    {
        public override bool ExecuteRule(World world, Cell cell)
        {
            bool resultFirst = childrenRules[0].ExecuteRule(world, cell);
            if(resultFirst)
            {
                List<IRule> rest = childrenRules.Skip(1).ToList();
                foreach(IRule rule in rest)
                {
                    rule.ExecuteRule(world, cell);
                }
                return true;
            }
            return false;
        }

        public override string ToString()
        {
            string s = "IfFirstThenRestRule +\n" + base.ToString();
            return s;
        }
    }
}