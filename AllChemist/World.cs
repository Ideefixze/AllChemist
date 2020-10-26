using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;


namespace AllChemist
{
    public class OnWorldChangeArgs
    {
        public World World { get; set; }

        public OnWorldChangeArgs(World w) { World = w; }
    }
    public class World
    {
        public Vector2Int TableSize { get; private set; }
        public CellTable CurrentTable { get; private set; }
        public CellTable NextIterationTable { get; private set; }

        public CellTypeBank CellTypeBank { get; set; }

        public event System.EventHandler<OnWorldChangeArgs> OnWorldChange; //Observer pattern

        public void Step()
        {
            for(int i = 0; i<TableSize.X;i++)
            {
                for(int j = 0; j<TableSize.Y;j++)
                {
                    CurrentTable.Cells[i, j].ExecuteRules(this);
                }
            }

            CurrentTable = NextIterationTable;
            NextIterationTable = new CellTable(TableSize, CurrentTable.DefaultCellType);
            ApplyChanges();
        }

        public void Paint(Vector2Int pos, CellType c)
        {
            CurrentTable.PlaceCell(pos, c);
        }

        public void ApplyChanges()
        {
            OnWorldChange.Invoke(this, new OnWorldChangeArgs(this));
        }

        public World(Vector2Int tableSize, CellTypeBank ctb)
        {
            this.CellTypeBank = ctb;
            this.TableSize = tableSize;
            CurrentTable = new CellTable(this.TableSize, CellTypeBank.GetDefaultCellType());
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
            ApplyChanges();
        }
    }
}