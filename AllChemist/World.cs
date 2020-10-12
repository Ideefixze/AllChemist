using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace AllChemist
{
    class World
    {
        public Vector2Int tableSize { get; private set; }
        public CellTable currentTable { get; private set; }
        public CellTable nextIterationTable { get; private set; }

        public void Step()
        {
            for(int i = 0; i<tableSize.x;i++)
            {
                for(int j = 0; j<tableSize.y;j++)
                {
                    currentTable.cells[i, j].ExecuteRules(this);
                }
            }

            currentTable = nextIterationTable;
            nextIterationTable = new CellTable(tableSize, currentTable.nullCell);
        }

        public World(Vector2Int tableSize, ExistingCell nullCell)
        {
            this.tableSize = tableSize;
            currentTable = new CellTable(this.tableSize, nullCell);
            nextIterationTable = new CellTable(this.tableSize, nullCell);
        }
    }
}