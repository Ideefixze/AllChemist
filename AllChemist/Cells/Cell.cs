using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using System.Windows.Media;
using AllChemist.Model;
using Newtonsoft.Json;

namespace AllChemist.Cells
{
    /// <summary>
    /// Base Cell class for all cells. Uses Prototype Design Pattern and Strategy for rules.
    /// </summary>
    abstract public class Cell 
    {

        public Vector2Int Position { get; set; }
        
        public Cell(Vector2Int startingPosition = new Vector2Int())
        {
            Position = startingPosition;
        }
        //Execute step change of this cell for a world.
        public abstract void ExecuteRules(World world); 
        public abstract Cell Clone();
    }
}
