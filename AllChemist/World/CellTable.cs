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
        public ExistingCell[,] Cells { get; private set; } //Cells in the 2D world. All cells in the 2D world have to be occupied.

        public Dictionary<int, int> GetNeighboursNumber(Vector2Int position)
        {
            Dictionary<int, int> neighbours = new Dictionary<int, int>();
            for (int x = position.X - 1; x <=position.X + 1; x++)
            { 
                for(int y = position.Y - 1; y<=position.Y+1;y++)
                {
                    if (x >= 0 && x < Size.X && y >= 0 && y < Size.Y && !(x==position.X && y==position.Y))
                    {
                        if(neighbours.ContainsKey(Cells[x, y].CellType.id))
                        {
                            neighbours[Cells[x, y].CellType.id]++;
                        }
                        else
                            neighbours.Add(Cells[x, y].CellType.id, 1);
                    }
                }
            }
            return neighbours;
            
        }

        public void PlaceCell(Vector2Int position, CellType cellType)
        {
            if (position.X >= 0 && position.X < Size.X && position.Y >= 0 && position.Y < Size.Y)
            {
                Cells[position.X, position.Y] = new ExistingCell(cellType);
                Cells[position.X, position.Y].Position = position;
            }
        }
        public CellTable(Vector2Int size, CellType cellType)
        {
            Size = size;
            DefaultCellType = cellType;
            Cells = new ExistingCell[size.X, size.Y];
            //MetaCells = new MetaCell[size.x, size.y];

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