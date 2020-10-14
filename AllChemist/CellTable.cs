using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace AllChemist
{
    public class CellTable
    {
        public Vector2Int Size { get; private set; }
        public ExistingCell NullCell { get; private set; } //Default, null cell, that represents "void" in the world. 
        public ExistingCell[,] Cells { get; private set; } //Cells in the 2D world. All cells in the 2D world have to be occupied.
        public List<MetaCell> MetaCells { get; private set; }


        public void PlaceCell(Vector2Int position, ExistingCell cell)
        {
            Cells[position.x, position.y] = new ExistingCell(cell);
            Cells[position.x, position.y].Position = position;
        }
        public CellTable(Vector2Int size, ExistingCell nullCell)
        {
            Size = size;
            NullCell = nullCell;
            Cells = new ExistingCell[size.x, size.y];

            for (int i = 0; i < size.x; i++)
            {
                for (int j = 0; j < size.y; j++)
                {
                    PlaceCell(new Vector2Int(i, j), nullCell);
                }
            }
        }

        public CellTable(CellTable ct)
        {
            Size = ct.Size;
            NullCell = ct.NullCell;
            Cells = new ExistingCell[Size.x, Size.y];
            for (int i = 0; i < Size.x; i++)
            {
                for (int j = 0; j < Size.y; j++)
                {
                    PlaceCell(new Vector2Int(i, j), ct.Cells[i, j]);
                }
            }
        }
    }
}