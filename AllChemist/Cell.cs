﻿using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using System.Windows.Media;
using Newtonsoft.Json;

namespace AllChemist
{
    [JsonObject(MemberSerialization.Fields)]
    public struct CellType
    {
       
        public readonly int id;

        public readonly string name;

        public readonly Color color;
        static private int idCounter=0;

        [JsonProperty]
        public CellBehaviour CellBehaviour { get; private set; }

        public CellType(string name, Color color)
        {
            this.name = name;
            this.color = color;
            this.id = idCounter++;
            CellBehaviour = new CellBehaviour();
        }

        public CellType(string name, byte R, byte G, byte B)
        {
            this.name = name;
            this.color = new Color();
            this.color.R = R;
            this.color.G = G;
            this.color.B = B;
            this.color.A = 255;
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
                return ct.id == this.id && ct.color == this.color;
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

        public Vector2Int Position { get; set; }
        
        public Cell(Vector2Int startingPosition = new Vector2Int())
        {
            Position = startingPosition;
        }

        public abstract void ExecuteRules(World world); //Execute some kind of rule of this cell for a world
        public abstract Cell Clone();
    }
}
