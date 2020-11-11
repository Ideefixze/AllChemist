using System;
using System.Collections.Generic;
using System.Text;

namespace AllChemist.Cells.Ruleset
{
    /// <summary>
    /// Interface for generating Ruleset.
    /// </summary>
    public interface IRulesetCreator
    {
        public Ruleset CreateRuleset();
    }
}
