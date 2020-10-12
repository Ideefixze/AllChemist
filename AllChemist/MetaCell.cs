using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace AllChemist
{
    /// <summary>
    /// Theoretical cell that doesn't "occupy" space in the world but can be used for description of different world cells.
    /// </summary>
    class MetaCell : Cell 
    {
        public string tag; //Tag that describes this MetaCell, e.g.: "Temperature (Celcius)"
        public Dictionary<string, double> properties { get; private set; }

        public MetaCell(Vector2Int startingPosition) : base(startingPosition) 
        {
            properties = new Dictionary<string, double>();
        }
        public MetaCell(MetaCell cell) : base(cell.position)
        {
            this.properties = new Dictionary<string, double>(cell.properties);
        }

        public override Cell Clone()
        {
            return new MetaCell(this);
        }
    }
}