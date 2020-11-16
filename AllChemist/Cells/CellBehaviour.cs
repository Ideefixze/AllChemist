using AllChemist.Cells.Rules;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace AllChemist.Cells
{
    /// <summary>
    /// Contains all data related for a cell behaviour in a step-model such as rules.
    /// </summary>
    [JsonObject]
    public class CellBehaviour
    {
        public List<IRule> Rules { get; private set; } //Rule container.

        public CellBehaviour(CellBehaviour cb)
        {
            Rules = new List<IRule>(cb.Rules);
        }

        public CellBehaviour()
        {
            Rules = new List<IRule>();
        }

        public override string ToString()
        {
            string s = "";
            foreach (IRule rule in Rules)
            {
                s += rule.ToString() + "\n";
            }
            return s;
        }
    }
}