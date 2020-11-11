using AllChemist.Model;
using Newtonsoft.Json;

namespace AllChemist.Cells.Rules
{
    [JsonObject]
    public interface IRule
    {
        /// <summary>
        /// Rule of a cell that will change the world.
        /// </summary>
        /// <param name="world">Game's world</param>
        /// <param name="cell">Cell this rule is attached to</param>
        /// <returns>True if rule made any changes in the world, False if there were no changes.</returns>
        bool ExecuteRule(World world, Cell cell);

    }
}