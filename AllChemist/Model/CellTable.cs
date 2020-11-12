using AllChemist.Cells;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace AllChemist.Model
{
    /// <summary>
    /// CellTable that contains a 2D array of ExistingCells
    /// </summary>
 
    public class CellTable
    {
        public Vector2Int Size { get; private set; }
        public CellType DefaultCellType { get; private set; } //Default, null cell, that represents "void" in the world. 
        public ExistingCell[,] Cells { get; private set; } //Cells in the 2D world. All cells in the 2D world have to be occupied.

        public List<ExistingCell> GetNeighbours(Vector2Int position, Func<ExistingCell, bool> predicate = null)
        {
            List<ExistingCell> neighbours = new List<ExistingCell>();
            if(predicate == null)
            {
                predicate = a => a!=null; 
            }
            for (int x = position.X - 1; x <= position.X + 1; x++)
            {
                for (int y = position.Y - 1; y <= position.Y + 1; y++)
                {
                    if (x >= 0 && x < Size.X && y >= 0 && y < Size.Y && !(x == position.X && y == position.Y))
                    {
                        if (predicate(Cells[x, y]))
                        {
                            neighbours.Add(Cells[x, y]);
                        }
                    }
                }
            }

            return neighbours;
        }

        public bool PlaceCell(Vector2Int position, CellType cellType)
        {
            if (position.X >= 0 && position.X < Size.X && position.Y >= 0 && position.Y < Size.Y)
            {
                Cells[position.X, position.Y] = new ExistingCell(cellType);
                Cells[position.X, position.Y].Position = position;
                return true;
            }
            return false;
        }
        public CellTable(Vector2Int size, CellType cellType)
        {
            Size = size;
            DefaultCellType = cellType;
            Cells = new ExistingCell[size.X, size.Y];

            for (int i = 0; i < size.X; i++)
            {
                for (int j = 0; j < size.Y; j++)
                {
                    PlaceCell(new Vector2Int(i, j), DefaultCellType);
                }
            }
        }

        public CellTable(CellTable ct)
        {
            Size = ct.Size;
            DefaultCellType = ct.DefaultCellType;
            Cells = new ExistingCell[Size.X, Size.Y];

            for (int i = 0; i < Size.X; i++)
            {
                for (int j = 0; j < Size.Y; j++)
                {
                    PlaceCell(new Vector2Int(i, j), ct.Cells[i,j].CellType);
                }
            }
        }
    }
}