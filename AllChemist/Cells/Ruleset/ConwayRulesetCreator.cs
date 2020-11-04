using AllChemist.Cells.Rules;
using System;
using System.Collections.Generic;
using System.Text;

namespace AllChemist.Cells.Ruleset
{
    public class ConwayRulesetCreator : IRulesetCreator
    {
        private string conwayRules;
        public ConwayRulesetCreator(string conwayRules)
        {
            this.conwayRules = conwayRules;
        }
        public Ruleset CreateRuleset()
        {
            string[] rules = conwayRules.Trim().Split('/');

            HashSet<int> aliveNCounts = new HashSet<int>();
            foreach(char c in rules[0])
            {
                aliveNCounts.Add(c -'0') ;
            }

            HashSet<int> deadNCounts = new HashSet<int>();
            foreach (char c in rules[1])
            {
                deadNCounts.Add(c - '0');
            }

            Ruleset ruleset = new Ruleset(System.Security.Principal.WindowsIdentity.GetCurrent().Name, conwayRules);

            CellType deadType = new CellType("Dead", 255, 255, 255);
            CellType aliveType = new CellType("Alive", 0, 0, 0);

            deadType.CellBehaviour.Rules.Add(new NeighbourChangeTo(1, deadNCounts, 1));
            aliveType.CellBehaviour.Rules.Add(new SwapToRule(0));
            aliveType.CellBehaviour.Rules.Add(new NeighbourChangeTo(1, aliveNCounts, 1));

            ruleset.CellTypeBank.CellTypes.Add(deadType.id, deadType);
            ruleset.CellTypeBank.CellTypes.Add(aliveType.id, aliveType);

            CellType.ResetCounter();

            return ruleset;
        }
    }
}
