using AllChemist.Cells.Rules;
using AllChemist.GUI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace AllChemist.Cells.Ruleset
{
    /// <summary>
    /// Creates a rules using a Conway rule notation.
    /// </summary>
    public class ConwayRulesetCreator : RulesetInterpreter
    {
        protected string conwayRules;
        public ConwayRulesetCreator(string conwayRules)
        {
            this.conwayRules = conwayRules;
        }

        public override Ruleset CreateRuleset()
        {
            try
            {

                if (conwayRules.Any(c => c != '/' && (c < '1' || c > '8')))
                    throw new Exception();

                string[] rules = conwayRules.Trim().Split('/');

                if (rules.Length != 2)
                    throw new Exception();

                List<int> aliveNCounts = new List<int>();
                foreach (char c in rules[0])
                {
                    aliveNCounts.Add(c - '0');
                }

                List<int> deadNCounts = new List<int>();
                foreach (char c in rules[1])
                {
                    deadNCounts.Add(c - '0');
                }

                Ruleset ruleset = new Ruleset(Environment.UserName, conwayRules);
                CellType.ResetCounter();
                CellType deadType = new CellType("Dead", 255, 255, 255);
                CellType aliveType = new CellType("Alive", 0, 0, 0);

                deadType.CellBehaviour.Rules.Add(new NeighbourChangeToRule(1, deadNCounts, 1));
                aliveType.CellBehaviour.Rules.Add(new SwapToRule(0));
                aliveType.CellBehaviour.Rules.Add(new NeighbourChangeToRule(1, aliveNCounts, 1));

                ruleset.CellTypeBank.CellTypes.Add(deadType.Id, deadType);
                ruleset.CellTypeBank.CellTypes.Add(aliveType.Id, aliveType);

                return ruleset;
            }
            catch
            {
                Console.WriteLine("Bad format. Loading default 23/3 rule.");
                conwayRules = "23/3";
                return CreateRuleset();
            }
        }


        /// <summary>
        /// Shows up a dialog for a user to input a ruleset.
        /// </summary>
        public override void LoadRuleset()
        {
            this.conwayRules = DialogInput.StringForm("Conway's Game of Life Creator", "(survive count)/(birth count)", "23/3");
        }
    }
}
