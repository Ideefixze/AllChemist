using AllChemist.Model;

namespace AllChemist.Cells
{
    /// <summary>
    /// Base Cell class for all cells. Uses Prototype Design Pattern and Strategy for rules.
    /// </summary>
    abstract public class Cell
    {

        public Vector2Int Position { get; set; }

        public Cell(Vector2Int startingPosition = new Vector2Int())
        {
            Position = startingPosition;
        }
        //Execute step change of this cell for a world.
        public abstract void ExecuteRules(World world);
        public abstract Cell Clone();
    }
}
