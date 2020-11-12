using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using AllChemist.Cells.Rules;
using AllChemist.Model;
using Newtonsoft.Json;

namespace AllChemist.Cells.Rules
{
    /// <summary>
    /// In next interation this cell swaps to a cell of a different id.
    /// </summary>

    [JsonObject]
    public abstract class CompositeRule:IRule
    {
        [JsonProperty]
        protected List<IRule> childrenRules;
        public CompositeRule()
        {
            
        }
        public virtual bool ExecuteRule(World world, Cell cell)
        {
            return false;
        }

        public override string ToString()
        {
            string s = "";
            childrenRules?.ForEach(rule => s += rule.ToString()+"\n");
            return s;
        }
    }
}