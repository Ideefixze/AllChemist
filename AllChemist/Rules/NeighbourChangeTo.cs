using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Newtonsoft.Json;

namespace AllChemist
{
    /// <summary>
    /// If there are N neighbours of type T, change to type T
    /// </summary>
    
    [JsonObject]
    public class NeighbourChangeTo:IRule
    {
        [JsonProperty]
        private int neighbourId;
        private int neighbourCount;
        public NeighbourChangeTo(int id, int n)
        {
            neighbourId = id;
            neighbourCount = n;
        }
        public bool ExecuteRule(World world, Cell cell)
        {
            Dictionary<int, int> neighbours = world.CurrentTable.GetNeighboursNumber(cell.Position);
            if (neighbours.ContainsKey(neighbourId))
            {
                if(neighbours[neighbourId]==neighbourCount)
                {
                    world.NextIterationTable.PlaceCell(cell.Position, world.CellTypeBank.CellTypes[neighbourId]);
                }
            }
            return true;
        }
    }
}