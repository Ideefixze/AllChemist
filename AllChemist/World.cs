using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;


namespace AllChemist
{
    public class OnWorldStepArgs
    {
        public World World { get; set; }

        public OnWorldStepArgs(World w) { World = w; }
    }
    public class World
    {
        public Vector2Int TableSize { get; private set; }
        public CellTable CurrentTable { get; private set; }
        public CellTable NextIterationTable { get; private set; }

        public event System.EventHandler<OnWorldStepArgs> OnWorldStep; //Observer pattern

        public void Step()
        {
            for(int i = 0; i<TableSize.x;i++)
            {
                for(int j = 0; j<TableSize.y;j++)
                {
                    CurrentTable.Cells[i, j].ExecuteRules(this);
                }
            }

            CurrentTable = NextIterationTable;
            NextIterationTable = new CellTable(TableSize, CurrentTable.NullCell);
            OnWorldStep.Invoke(this, new OnWorldStepArgs(this));
        }

        public World(Vector2Int tableSize, ExistingCell nullCell)
        {
            this.TableSize = tableSize;
            CurrentTable = new CellTable(this.TableSize, nullCell);
            NextIterationTable = new CellTable(this.TableSize, nullCell);
        }
    }
}