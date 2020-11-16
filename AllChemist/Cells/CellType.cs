using AllChemist.Serialization;
using Newtonsoft.Json;
using System;
using System.Windows.Media;

namespace AllChemist.Cells
{
    /// <summary>
    /// Type of a cell. Instead of holding all data in a single cell, we use predefined types of cells.
    /// </summary>
    [JsonObject(MemberSerialization.Fields)]
    public struct CellType
    {

        public readonly int Id;

        public readonly string Name;

        public readonly Color Color;
        static private int idCounter = 0; //All ids are accordingly set in the order they are initialized

        public static void ResetCounter()
        {
            idCounter = 0;
        }

        [JsonProperty]
        public CellBehaviour CellBehaviour { get; private set; }

        public CellType(string name, Color color)
        {
            this.Name = name;
            this.Color = color;
            this.Id = idCounter++;
            CellBehaviour = new CellBehaviour();
        }

        public CellType(string name, byte R, byte G, byte B)
        {
            this.Name = name;
            this.Color = new Color();
            this.Color.R = R;
            this.Color.G = G;
            this.Color.B = B;
            this.Color.A = 255;
            this.Id = idCounter++;
            CellBehaviour = new CellBehaviour();
        }

        public override int GetHashCode()
        {
            return JSONHandler.Save(this).GetHashCode();
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
                return ct.Id == this.Id && ct.Color == this.Color;
            }
        }

        public static bool operator ==(CellType cta, CellType ctb)
        {
            return cta.Id == ctb.Id;
        }

        public static bool operator !=(CellType cta, CellType ctb)
        {
            return cta.Id != ctb.Id;
        }

        public override string ToString()
        {
            return this.Id + " " + this.Name + " " + this.Color + "\n" + CellBehaviour.ToString();
        }
    }
}