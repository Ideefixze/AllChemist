using AllChemist.Cells;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;


namespace AllChemist.Model
{

    public enum EPaintType
    {
        PAINT_CURRENT,
        PAINT_NEXT
    }
    public class DrawWorldArgs
    {
        public CellTable CellTable;
        public HashSet<Vector2Int> Delta;

        public DrawWorldArgs(World world) { 
            CellTable = world.CurrentTable; 
            Delta = new HashSet<Vector2Int>(world.Delta); 
        }
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
            lock (this)
            {
                Stopwatch t = new Stopwatch();
                t.Start();
                for (int i = 0; i < TableSize.X; i++)
                {
                    for (int j = 0; j < TableSize.Y; j++)
                    {
                        CurrentTable.Cells[i, j].ExecuteRules(this);
                    }
                }

                HashSet<Vector2Int> CellTypeChange = new HashSet<Vector2Int>();
                foreach(Vector2Int change in Delta)
                {
                    if (CurrentTable.Cells[change.X, change.Y].CellType != NextIterationTable.Cells[change.X, change.Y].CellType)
                        CellTypeChange.Add(change);
                }
                Delta = new HashSet<Vector2Int>(CellTypeChange);

                CurrentTable = NextIterationTable;
                NextIterationTable = new CellTable(TableSize, CurrentTable.DefaultCellType);
                ApplyChanges();
                t.Stop();
                Console.WriteLine("Model has been updated in " + t.Elapsed.TotalSeconds + "s");
            }
        }

        public bool Paint(Vector2Int pos, CellType c, EPaintType paintType=EPaintType.PAINT_NEXT)
        {
            bool result = false;
            CellType previous=CurrentTable.Cells[pos.X,pos.Y].CellType;
            switch(paintType)
            {
                case EPaintType.PAINT_NEXT:
                    result = NextIterationTable.PlaceCell(pos, c);
                    break;
                case EPaintType.PAINT_CURRENT:
                    result = CurrentTable.PlaceCell(pos, c);
                    break;
            }

            if (result && previous!=c)
            {
                Delta.Add(pos);
                return true;
            }
            return false;
                
        }

        public void ApplyChanges()
        {
            OnWorldChange?.Invoke(this, new DrawWorldArgs(this));
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
            lock(this)
            {
                Delta.Clear();
                CurrentTable = memento;
                NextIterationTable = new CellTable(this.TableSize, memento.DefaultCellType);
                for (int i = 0; i < TableSize.X; i++)
                {
                    for (int j = 0; j < TableSize.Y; j++)
                    {
                        Delta.Add(new Vector2Int(i, j));
                    }
                }
                ApplyChanges();
            }

        }
    }
}