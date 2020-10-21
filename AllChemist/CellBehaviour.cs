using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Newtonsoft.Json;

namespace AllChemist
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
    }
}