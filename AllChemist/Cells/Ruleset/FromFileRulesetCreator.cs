using AllChemist.SerializationAndIO;

namespace AllChemist.Cells.Ruleset
{
    /// <summary>
    /// Loads up a custom ruleset from a file.
    /// </summary>
    public class FromFileRulesetCreator : RulesetInterpreter
    {
        public JSONFileDeserializer<Ruleset> JSONFileDeserializer;
        private bool returned = false;

        public FromFileRulesetCreator()
        {
            JSONFileDeserializer = new JSONFileDeserializer<Ruleset>("Data\\Rulesets", "default.json");
        }

        public override void LoadRuleset()
        {
            JSONFileDeserializer.ShowOpenFileDialog();
        }
        public override Ruleset CreateRuleset()
        {
            return JSONFileDeserializer.GetData();
        }
    }
}
