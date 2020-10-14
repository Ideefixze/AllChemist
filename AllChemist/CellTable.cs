using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace AllChemist
{
    public class CellTable
    {
        public Vector2Int Size { get; private set; }
        public CellType DefaultCellType { get; private set; } //Default, null cell, that represents "void" in the world. 

        public CellType DefaultMetaCellType { get; private set; } //Default, null cell, that represents "void" in the world. 
        public ExistingCell[,] Cells { get; private set; } //Cells in the 2D world. All cells in the 2D world have to be occupied.
        public MetaCell[,] MetaCells { get; private set; } //MetaCells

        public void PlaceCell(Vector2Int position, CellType cellType)
        {
            Cells[position.x, position.y] = new ExistingCell(cellType);
            Cells[position.x, position.y].Position = position;
        }
        public CellTable(Vector2Int size, CellType cellType, CellType metaCellType)
        {
            Size = size;
            DefaultCellType = cellType;
            DefaultMetaCellType = metaCellType;
            Cells = new ExistingCell[size.x, size.y];
            MetaCells = new MetaCell[size.x, size.y];

            for (int i = 0; i < size.x; i++)
            {
                for (int j = 0; j < size.y; j++)
                {
                    PlaceCell(new Vector2Int(i, j), DefaultCellType);
                    MetaCells[i, j] = new MetaCell(DefaultMetaCellType, new Vector2Int(i, j));
                }
            }
        }

        public CellTable(CellTable ct)
        {
            Size = ct.Size;
            DefaultCellType = ct.DefaultCellType;
            DefaultMetaCellType = ct.DefaultMetaCellType;
            Cells = new ExistingCell[Size.x, Size.y];
            MetaCells = new MetaCell[Size.x, Size.y];

            for (int i = 0; i < Size.x; i++)
            {
                for (int j = 0; j < Size.y; j++)
                {
                    PlaceCell(new Vector2Int(i, j), DefaultCellType);
                    MetaCells[i, j] = new MetaCell(DefaultMetaCellType, new Vector2Int(i, j));
                }
            }
        }
    }
}