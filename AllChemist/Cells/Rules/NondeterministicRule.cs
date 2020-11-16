using AllChemist.Model;
using Newtonsoft.Json;

namespace AllChemist.Cells.Rules
{
    /// <summary>
    /// IRule extension. There will be some chance for a given rule to be executed.
    /// </summary>
    [JsonObject]
    class NondeterministicRule : IRule
    {
        [JsonProperty]
        private IRule rule;
        [JsonProperty]
        private double chance;
        public bool ExecuteRule(World world, Cell cell)
        {
            if (App.Random.NextDouble() <= chance)
            {
                return rule.ExecuteRule(world, cell);
            }
            return false;
        }

        public override string ToString()
        {
            return $"NondeterministicRule ({chance}) on rule: {rule.ToString()}";
        }
    }
}
