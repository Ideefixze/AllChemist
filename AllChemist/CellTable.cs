using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace AllChemist
{
    class CellTable
    {
        public Vector2Int size { get; private set; }
        public ExistingCell nullCell { get; private set; } //Default, null cell, that represents "void" in the world. 
        public ExistingCell[,] cells { get; private set; } //Cells in the 2D world. All cells in the 2D world have to be occupied.
        public List<MetaCell> metaCells { get; private set; }

        public CellTable(Vector2Int size, ExistingCell nullCell)
        {
            this.size = size;
            this.nullCell = nullCell;
            this.cells = new ExistingCell[size.x, size.y];

            for (int i = 0; i < size.x; i++)
            {
                for (int j = 0; j < size.y; j++)
                {
                    cells[i, j] = new ExistingCell(nullCell);
                }
            }
        }

        public CellTable(CellTable ct)
        {
            this.size = ct.size;
            this.nullCell = ct.nullCell;
            this.cells = new ExistingCell[size.x, size.y];
            for (int i = 0; i < size.x; i++)
            {
                for (int j = 0; j < size.y; j++)
                {
                    cells[i, j] = new ExistingCell(ct.cells[i, j]); 
                }
            }
        }
    }
}