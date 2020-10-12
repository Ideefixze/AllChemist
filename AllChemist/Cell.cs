using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace AllChemist
{
    interface IRule
    {
        /// <summary>
        /// Rule of a cell that will change world.
        /// </summary>
        /// <param name="world">Game's world</param>
        /// <param name="cell">Cell this rule is attached to</param>
        /// <returns>True if rule made any changes in the world, False if there were no changes.</returns>
        bool ExecuteRule(World world, Cell cell); 

    }

    /// <summary>
    /// In next interation this cell remains as it is.
    /// </summary>
    class RemainRule:IRule
    {
        public bool ExecuteRule(World world, Cell cell)
        {
            world.nextIterationTable.cells[cell.position.x, cell.position.y] = new ExistingCell(world.currentTable.cells[cell.position.x, cell.position.y]);
            return true;
        }
    }

    class CellBehaviour
    {
        public List<IRule> rules { get; private set; } //Strategy pattern
    }

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


    /// <summary>
    /// Existing Cell that is used to be filled in the World's grid.
    /// </summary>
    class ExistingCell : Cell
    {
        public string cellArt { get; private set; }

        public ExistingCell(Vector2Int startingPosition, string cellArt) : base(startingPosition)
        {
            this.cellArt = cellArt;
        }

        public ExistingCell(ExistingCell cell) : base(cell.position)
        {
            this.cellArt = new string(cell.cellArt);
        }

        public override Cell Clone()
        {
            return new ExistingCell(this);
        }
    }

    /// <summary>
    /// Theoretical cell that doesn't "occupy" space in the world but can be used for description of different world cells.
    /// </summary>
    class MetaCell : Cell 
    {
        public string tag; //Tag that describes this MetaCell, e.g.: "Temperature (Celcius)"
        public Dictionary<string, double> properties { get; private set; }

        public MetaCell(Vector2Int startingPosition) : base(startingPosition) 
        {
            properties = new Dictionary<string, double>();
        }
        public MetaCell(MetaCell cell) : base(cell.position)
        {
            this.properties = new Dictionary<string, double>(cell.properties);
        }

        public override Cell Clone()
        {
            return new MetaCell(this);
        }
    }
    /// <summary>
    /// Base Cell class for all cells. Uses Prototype Design Pattern and Strategy for rules.
    /// </summary>
    abstract class Cell 
    {
        public Vector2Int position { get; private set; }
        public CellBehaviour cellBehaviour { get; private set; }

        public Cell(Vector2Int startingPosition)
        {
            position = startingPosition;
        }

        public void ExecuteRules(World world)
        {
            foreach(IRule rule in cellBehaviour.rules)
            {
                rule.ExecuteRule(world, this); //Execute all strategies that this cell has
            }
        }
        public abstract Cell Clone();
    }
}
