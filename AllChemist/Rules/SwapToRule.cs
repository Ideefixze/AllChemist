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
            world.NextIterationTable.Cells[cell.Position.X, cell.Position.Y] = new ExistingCell(world.CellTypeBank.CellTypes[cellToSwapId], cell.Position);
            return true;
        }
    }
}