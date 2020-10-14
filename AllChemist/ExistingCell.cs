using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace AllChemist
{
    /// <summary>
    /// Existing Cell that is used to be filled in the World's grid.
    /// </summary>
    public class ExistingCell : Cell
    {

        public ExistingCell(CellType cellType, Vector2Int startingPosition=new Vector2Int()) : base(cellType, startingPosition)
        {
            
        }

        public ExistingCell(ExistingCell cell) : base(cell.CellType, cell.Position)
        {
            
        }

        public override Cell Clone()
        {
            return new ExistingCell(this);
        }
    }
}