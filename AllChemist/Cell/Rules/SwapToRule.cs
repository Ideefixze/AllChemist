using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Newtonsoft.Json;

namespace AllChemist
{
    /// <summary>
    /// In next interation this cell remains as it is.
    /// </summary>
    
    [JsonObject]
    public class SwapToRule:IRule
    {
        [JsonProperty]
        private int cellToSwapId;
        public SwapToRule(int id)
        {
            cellToSwapId = id;
        }
        public bool ExecuteRule(World world, Cell cell)
        {
            world.PaintNextStep(cell.Position, world.CellTypeBank.CellTypes[cellToSwapId]);
            return true;
        }

        public override string ToString()
        {
            return "SwapToRule " + cellToSwapId.ToString();
        }
    }
}