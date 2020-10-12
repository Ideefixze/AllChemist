using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace AllChemist
{
    /// <summary>
    /// In next interation this cell remains as it is.
    /// </summary>
    class RemainRule:IRule
    {
        public bool ExecuteRule(World world, Cell cell)
        {
            world.nextIterationTable.cells[cell.position.x, cell.position.y] = new ExistingCell(world.currentTable.cells[cell.position.x, cell.position.y]);
            return true;
        }
    }
}