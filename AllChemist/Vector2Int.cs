using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace AllChemist
{
    public struct Vector2Int
    {
        public int x, y;

        public Vector2Int(int x=0, int y=0)
        {
            this.x = x;
            this.y = y;
        }

        public Vector2Int(Vector2Int v)
        {
            this.x = v.x;
            this.y = v.y;
        }

        public static bool operator==(Vector2Int a, Vector2Int b)
        {
            return a.x == b.x && a.y == b.y;
        }

        public static bool operator !=(Vector2Int a, Vector2Int b)
        {
            return !(a==b);
        }


    }
}