using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace AllChemist
{
    /// <summary>
    /// Base Cell class for all cells. Uses Prototype Design Pattern and Strategy for rules.
    /// </summary>
    abstract class Cell 
    {
        public Vector2Int position { get; private set; }
        public CellBehaviour cellBehaviour { get; private set; }

        public Cell(Vector2Int startingPosition)
        {
            position = startingPosition;
        }

        public void ExecuteRules(World world)
        {
            foreach(IRule rule in cellBehaviour.rules)
            {
                rule.ExecuteRule(world, this); //Execute all strategies that this cell has
            }
        }
        public abstract Cell Clone();
    }
}
