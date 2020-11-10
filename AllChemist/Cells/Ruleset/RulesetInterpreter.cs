using System;
using System.Collections.Generic;
using System.Text;

namespace AllChemist.Cells.Ruleset
{
    public abstract class RulesetInterpreter : IRulesetCreator, IRulesetLoader
    {
        public abstract Ruleset CreateRuleset();

        public abstract void LoadRuleset();
    }
}
