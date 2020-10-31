using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;


namespace AllChemist
{
    public class DrawWorldArgs
    {
        public CellTable CellTable;
        public HashSet<Vector2Int> Delta;

        public DrawWorldArgs(World world) { CellTable = new CellTable(world.CurrentTable); Delta = new HashSet<Vector2Int>(world.Delta); }
    }
    public class World
    {
        public Vector2Int TableSize { get; private set; }
        public CellTable CurrentTable { get; private set; }
        public CellTable NextIterationTable { get; private set; }

        public CellTypeBank CellTypeBank { get; set; }

        public HashSet<Vector2Int> Delta { get; private set; }

        public event System.EventHandler<DrawWorldArgs> OnWorldChange; //Observer pattern

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
            Delta.Add(pos);
        }

        public void PaintNextStep(Vector2Int pos, CellType c)
        {
            NextIterationTable.PlaceCell(pos, c);
            Delta.Add(pos);
        }

        public void ApplyChanges()
        {
            OnWorldChange.Invoke(this, new DrawWorldArgs(this));
            Delta.Clear();
        }

        public World(Vector2Int tableSize, CellTypeBank ctb)
        {
            this.OnWorldChange = null;
            this.CellTypeBank = ctb;
            this.TableSize = tableSize;
            CurrentTable = new CellTable(this.TableSize, CellTypeBank.GetDefaultCellType());
            NextIterationTable = new CellTable(CurrentTable);
            Delta = new HashSet<Vector2Int>();
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