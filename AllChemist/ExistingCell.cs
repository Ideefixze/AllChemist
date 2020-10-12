using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace AllChemist
{
    /// <summary>
    /// Existing Cell that is used to be filled in the World's grid.
    /// </summary>
    class ExistingCell : Cell
    {
        public string cellArt { get; private set; }

        public ExistingCell(Vector2Int startingPosition, string cellArt) : base(startingPosition)
        {
            this.cellArt = cellArt;
        }

        public ExistingCell(ExistingCell cell) : base(cell.position)
        {
            this.cellArt = new string(cell.cellArt);
        }

        public override Cell Clone()
        {
            return new ExistingCell(this);
        }
    }
}