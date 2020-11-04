using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using AllChemist.Model;
using AllChemist.Serialization;
using Newtonsoft.Json;

namespace AllChemist.Cells.Rules
{
    /// <summary>
    /// If there are N neighbours of type T, change to type T
    /// </summary>

    [JsonObject]
    public class NeighbourChangeTo : IRule
    {
        [JsonProperty]
        private int neighbourId;
        [JsonProperty]
        private HashSet<int> neighbourCounts;
        [JsonProperty]
        private int neighbourIdToChange;
        public NeighbourChangeTo(int id, HashSet<int> set, int to)
        {
            neighbourId = id;
            neighbourCounts = new HashSet<int>(set);
            neighbourIdToChange = to;
        }
        public bool ExecuteRule(World world, Cell cell)
        {
            int c = world.CurrentTable.GetNeighboursOfIdCount(cell.Position, neighbourId);
            if (neighbourCounts.Contains(c))
            {
                world.Paint(cell.Position, world.CellTypeBank.CellTypes[neighbourIdToChange]);
            }
            return true;
        }

        public override string ToString()
        {
            return $"NeighbourChangeTo {neighbourIdToChange} if {neighbourId} count: ({JSONHandler.Save(neighbourCounts)})";
        }
    }
}