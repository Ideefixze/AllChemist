using AllChemist.Cells.Rules;
using AllChemist.Model;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace AllChemist.Cells
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

        /// <summary>
        /// Executes rules associated with this CellType. Implementation of a Strategy Design Pattern (with a small twist because it uses composition).
        /// </summary>
        /// <param name="world">Model on which rules should be executed</param>
        public override void ExecuteRules(World world)
        {
            foreach (IRule rule in CellType.CellBehaviour.Rules)
            {
                rule.ExecuteRule(world, this);
            }
        }

        public override Cell Clone()
        {
            return new ExistingCell(this);
        }
    }
}