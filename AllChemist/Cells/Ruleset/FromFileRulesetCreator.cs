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
    /// <summary>
    /// Loads up a custom ruleset from a file.
    /// </summary>
    public class FromFileRulesetCreator : RulesetInterpreter
    {
        public JSONFileDeserializer<Ruleset> JSONFileDeserializer;
        private bool returned = false;

        public FromFileRulesetCreator()
        {
            JSONFileDeserializer = new JSONFileDeserializer<Ruleset>("rulesets", "default.json");
        }

        public override void LoadRuleset()
        {
            JSONFileDeserializer.ShowOpenFileDialog();
        }
        public override Ruleset CreateRuleset()
        { 
            return JSONFileDeserializer.GetData();
        }
    }
}
