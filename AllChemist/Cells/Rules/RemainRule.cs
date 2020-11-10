using AllChemist.Cells.Rules;
using AllChemist.Model;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace AllChemist.Cells.Rules
{
    /// <summary>
    /// In next interation this cell remains as it is.
    /// </summary>
    public class RemainRule:IRule
    {
        public bool ExecuteRule(World world, Cell cell)
        {
            return world.Paint(cell.Position, world.CurrentTable.Cells[cell.Position.X, cell.Position.Y].CellType); 
        }
    }
}