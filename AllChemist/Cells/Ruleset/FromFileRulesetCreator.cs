using AllChemist.GUI.Controllers;
using AllChemist.Serialization;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace AllChemist.Cells.Ruleset
{
    public class FromFileRulesetCreator : IRulesetCreator
    {
        public JSONFileDeserializer<Ruleset> JSONFileDeserializer;
        private bool returned = false;

        public FromFileRulesetCreator()
        {
            JSONFileDeserializer = new JSONFileDeserializer<Ruleset>("rulesets", "default.json");
        }
        public Ruleset CreateRuleset()
        {
            if(!returned)
            {
                JSONFileDeserializer.ShowOpenFileDialog();
            }
            returned = true;
            
            return JSONFileDeserializer.GetData();
        }
    }
}
