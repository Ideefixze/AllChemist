using AllChemist.Cells.Rules;
using AllChemist.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace AllChemist.Cells.Rules
{
    [Serializable]
    public abstract class CellRule : IRule
    {
        protected String RuleType;

        public CellRule()
        {
            RuleType = this.GetType().ToString();
        }
        public abstract bool ExecuteRule(World w, Cell c);

    }
}
