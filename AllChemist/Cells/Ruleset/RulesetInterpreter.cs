using System;
using System.Collections.Generic;
using System.Text;

namespace AllChemist.Cells.Ruleset
{
    /// <summary>
    /// Loads some data (file, Conway rules) associated with a ruleset and then generates a new ruleset.
    /// </summary>
    public abstract class RulesetInterpreter : IRulesetCreator, IRulesetLoader
    {
        public abstract Ruleset CreateRuleset();

        public abstract void LoadRuleset();
    }
}
