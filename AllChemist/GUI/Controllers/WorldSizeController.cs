using System;
using System.Windows.Controls;

namespace AllChemist
{
    public class WorldSizeController
    {
        private TextBox textBoxX;
        private TextBox textBoxY;

        public WorldSizeController(TextBox x, TextBox y)
        {
            textBoxX = x;
            textBoxY = y;
        }

        public Vector2Int GetWorldSize()
        {
            return new Vector2Int(Math.Max(int.Parse(textBoxX.Text), 1), Math.Max(int.Parse(textBoxX.Text), 1));
        }
    }
}