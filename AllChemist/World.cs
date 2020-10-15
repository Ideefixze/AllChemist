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

        public static int steps=0;

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
            NextIterationTable = new CellTable(TableSize, CurrentTable.DefaultCellType);
            OnWorldStep.Invoke(this, new OnWorldStepArgs(this));
            steps++;
            System.Console.WriteLine(steps);
        }

        public World(Vector2Int tableSize, CellType defaultCellType)
        {
            this.TableSize = tableSize;
            CurrentTable = new CellTable(this.TableSize, defaultCellType);
            NextIterationTable = new CellTable(CurrentTable);
        }

        //Memento Design Pattern
        //TODO: Caretaker or maybe separate class for snapshot
        public CellTable CreateSnapshot()
        {
            return new CellTable(CurrentTable);
        }

        public void RestoreSnapshot(CellTable memento)
        {
            CurrentTable = memento;
            NextIterationTable = new CellTable(this.TableSize, memento.DefaultCellType);
        }
    }
}