using AllChemist.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace AllChemist.Cells.Rules
{
    [JsonObject]
    class NondeterministicRule : IRule
    {
        [JsonProperty]
        private IRule rule;
        [JsonProperty]
        private double chance;
        public bool ExecuteRule(World world, Cell cell)
        {
            if(App.Random.NextDouble()<=chance)
            {
                return rule.ExecuteRule(world, cell);
            }
            return false;
        }
    }
}
