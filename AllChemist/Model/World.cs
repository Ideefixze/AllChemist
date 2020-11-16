using AllChemist.Cells;
using System.Collections.Generic;
using System.Diagnostics;


namespace AllChemist.Model
{
    /// <summary>
    /// Painting mode.
    /// </summary>
    public enum EPaintType
    {
        PAINT_CURRENT,
        PAINT_NEXT
    }
    public class DrawWorldArgs
    {
        public CellTable CellTable;
        public HashSet<Vector2Int> Delta;
        public int Steps;

        //We don't use original world because other components may modify it without our knowledge
        public DrawWorldArgs(World world)
        {
            CellTable = world.CurrentTable;
            Delta = new HashSet<Vector2Int>(world.Delta);
            Steps = world.Steps;
        }
    }
    public class World
    {
        public int Steps;
        public Vector2Int TableSize { get; private set; }
        public CellTable CurrentTable { get; private set; }
        public CellTable NextIterationTable { get; private set; }

        public CellTypeBank CellTypeBank { get; set; } //Bank of used CellTypes

        public HashSet<Vector2Int> Delta { get; private set; } //Positions with all changes

        public event System.EventHandler<DrawWorldArgs> OnWorldChange; //Observer pattern

        /// <summary>
        /// Step for a simulation.
        /// </summary>
        public void Step()
        {
            lock (this)
            {
                Stopwatch t = new Stopwatch();
                t.Start();
                //Step and execution of all rules
                for (int i = 0; i < TableSize.X; i++)
                {
                    for (int j = 0; j < TableSize.Y; j++)
                    {
                        CurrentTable.Cells[i, j].ExecuteRules(this);
                    }
                }

                //Clears up all naive changes that turned out not to change anything
                HashSet<Vector2Int> CellTypeChange = new HashSet<Vector2Int>();
                foreach (Vector2Int change in Delta)
                {
                    if (CurrentTable.Cells[change.X, change.Y].CellType != NextIterationTable.Cells[change.X, change.Y].CellType)
                        CellTypeChange.Add(change);
                }
                Delta = new HashSet<Vector2Int>(CellTypeChange);

                //Sets up next iteration and sends all changes to the view
                CurrentTable = NextIterationTable;
                NextIterationTable = new CellTable(CurrentTable);
                Steps++;
                ApplyChanges();
                t.Stop();

                //Console.WriteLine("Model has been updated in " + t.Elapsed.TotalSeconds + "s");
            }
        }

        //Single pixel paint
        public bool Paint(Vector2Int pos, CellType c, EPaintType paintType = EPaintType.PAINT_NEXT)
        {
            if (pos.X < 0 || pos.X >= TableSize.X || pos.Y < 0 || pos.Y >= TableSize.Y)
                return false;

            CellType prev = CurrentTable.Cells[pos.X, pos.Y].CellType;
            bool result = false;
            switch (paintType)
            {
                case EPaintType.PAINT_NEXT:
                    result = NextIterationTable.PlaceCell(pos, c);
                    break;
                case EPaintType.PAINT_CURRENT:
                    result = CurrentTable.PlaceCell(pos, c);
                    break;
            }

            if (result && prev != c)
            {
                Delta.Add(pos);
            }
            return result;
        }

        //Sends all changes to the view
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
            Steps = 0;
        }

        //Memento Design Pattern
        public CellTable CreateSnapshot()
        {
            return new CellTable(CurrentTable);
        }

        public void RestoreSnapshot(CellTable memento)
        {
            lock (this)
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
                Steps = 0;
                ApplyChanges();
            }

        }
    }
}