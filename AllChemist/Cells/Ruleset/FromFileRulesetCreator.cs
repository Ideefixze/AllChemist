using AllChemist.GUI.Controllers;
using AllChemist.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace AllChemist.Cells.Ruleset
{
    public class FromFileRulesetCreator : IRulesetCreator
    {
        public JSONFileDeserializer<Ruleset> JSONFileDeserializer;

        public FromFileRulesetCreator()
        {
            JSONFileDeserializer = new JSONFileDeserializer<Ruleset>("rulesets", "default.json");
        }
        public Ruleset CreateRuleset()
        {
            return JSONFileDeserializer.GetData();
        }
    }
}
