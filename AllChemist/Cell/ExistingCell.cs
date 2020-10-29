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
        public CellType CellType { get; protected set; }

        public ExistingCell(CellType cellType, Vector2Int startingPosition=new Vector2Int()) : base(startingPosition)
        {
            CellType = cellType;
        }

        public ExistingCell(ExistingCell cell) : base(cell.Position)
        {
            CellType = cell.CellType;
        }

        public override void ExecuteRules(World world)
        {
            foreach (IRule rule in CellType.CellBehaviour.Rules)
            {
                rule.ExecuteRule(world, this); //Execute all strategies that this cell type has
            }
        }

        public override Cell Clone()
        {
            return new ExistingCell(this);
        }
    }
}