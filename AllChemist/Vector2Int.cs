namespace AllChemist
{
    /// <summary>
    /// Simple 2D mathematical vector.
    /// </summary>
    public struct Vector2Int
    {
        public int X, Y;

        public Vector2Int(int x = 0, int y = 0)
        {
            this.X = x;
            this.Y = y;
        }

        public Vector2Int(Vector2Int v)
        {
            this.X = v.X;
            this.Y = v.Y;
        }

        public static bool operator ==(Vector2Int a, Vector2Int b)
        {
            return a.X == b.X && a.Y == b.Y;
        }

        public static bool operator !=(Vector2Int a, Vector2Int b)
        {
            return !(a == b);
        }

        public override string ToString()
        {
            return $"({X} , {Y})";
        }


    }
}