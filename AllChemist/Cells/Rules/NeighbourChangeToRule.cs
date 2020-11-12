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
    public class NeighbourChangeToRule : IRule
    {
        [JsonProperty]
        private int neighbourId;
        [JsonProperty]
        public HashSet<int> neighbourCounts;
        [JsonProperty]
        private int neighbourIdToChange;

        //In code constructor for Conway Generator
        public NeighbourChangeToRule(int id, IEnumerable<int> list, int to)
        {
            neighbourId = id;
            neighbourCounts = new HashSet<int>(list);
            neighbourIdToChange = to;
        }
        //Json constructor because constructor above somehow doesn't like Newtonsoft's Json 
        public NeighbourChangeToRule()
        {

        }
        public bool ExecuteRule(World world, Cell cell)
        {
            int c = world.CurrentTable.GetNeighbours(cell.Position, cell => cell.CellType.Id==neighbourId).Count;
            if (neighbourCounts.Contains(c))
            {
                world.Paint(cell.Position, world.CellTypeBank.CellTypes[neighbourIdToChange]);
                return true;
            }
            return false;
            
        }

        public override string ToString()
        {
            return $"NeighbourChangeTo {neighbourIdToChange} if {neighbourId} count: ({JSONHandler.Save(neighbourCounts)})";
        }
    }
}