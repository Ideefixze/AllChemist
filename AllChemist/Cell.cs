using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace AllChemist
{
    public struct CellType
    {
        private readonly string art;
        private readonly int id;
        static private int idCounter=0;

        public CellBehaviour CellBehaviour { get; private set; }

        public CellType(string art)
        {
            this.art = art;
            this.id = idCounter++;
            CellBehaviour = new CellBehaviour();
        }

        public override int GetHashCode()
        {
            return id.GetHashCode();
        }

        public override bool Equals(Object obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                CellType ct = (CellType)obj;
                return ct.id == this.id && ct.art == this.art;
            } 
        }

        public static bool operator==(CellType cta, CellType ctb)
        {
            return cta.id == ctb.id;
        }

        public static bool operator!=(CellType cta, CellType ctb)
        {
            return cta.id != ctb.id;
        }
    }
    /// <summary>
    /// Base Cell class for all cells. Uses Prototype Design Pattern and Strategy for rules.
    /// </summary>
    abstract public class Cell 
    {
        public CellType CellType { get; protected set; }
        public Vector2Int Position { get; set; }
        

        public Cell(CellType cellType, Vector2Int startingPosition)
        {
            CellType = cellType;
            Position = startingPosition;
        }

        public void ExecuteRules(World world)
        {
            foreach (IRule rule in CellType.CellBehaviour.Rules)
            {
                rule.ExecuteRule(world, this); //Execute all strategies that this cell type has
            }
        }
        public abstract Cell Clone();
    }
}
