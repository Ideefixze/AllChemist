using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using AllChemist.Cells.Rules;
using AllChemist.Model;
using Newtonsoft.Json;

namespace AllChemist.Cells.Rules
{
    /// <summary>
    /// In next interation this cell swaps to a cell of a different id.
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
            return world.Paint(cell.Position, world.CellTypeBank.CellTypes[cellToSwapId]);
        }

        public override string ToString()
        {
            return "SwapToRule " + cellToSwapId.ToString();
        }
    }
}