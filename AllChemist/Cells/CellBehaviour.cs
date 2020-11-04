using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using AllChemist.Cells.Rules;
using Newtonsoft.Json;

namespace AllChemist.Cells
{
    [JsonObject]
    public class CellBehaviour
    {
        public List<IRule> Rules { get; private set; } //Strategy pattern

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
            foreach(IRule rule in Rules)
            {
                s += rule.ToString() + "\n";
            }
            return s;
        }
    }
}