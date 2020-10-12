using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace AllChemist
{
    public struct Vector2Int
    {
        public int x, y;

        public Vector2Int(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public Vector2Int(Vector2Int v)
        {
            this.x = v.x;
            this.y = v.y;
        }
    }
}