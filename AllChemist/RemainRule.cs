using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace AllChemist
{
    /// <summary>
    /// In next interation this cell remains as it is.
    /// </summary>
    public class RemainRule:IRule
    {
        public bool ExecuteRule(World world, Cell cell)
        {
            world.NextIterationTable.Cells[cell.Position.x, cell.Position.y] = new ExistingCell(world.CurrentTable.Cells[cell.Position.x, cell.Position.y]);
            return true;
        }
    }
}