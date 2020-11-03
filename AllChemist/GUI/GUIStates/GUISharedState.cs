using System;
using System.Collections.Generic;
using System.Text;

namespace AllChemist.GUI.GUIStates
{
    public class GUISharedState
    {
        private HashSet<string> flags;

        public GUISharedState()
        {
            flags = new HashSet<string>();
        }

        public bool AddFlag(string flagName)
        {
            if(flags.Contains(flagName)==false)
            {
                flags.Add(flagName);
                return true;
            }
            return false;
        }

        public bool RemoveFlag(string flagName)
        {
            if (flags.Contains(flagName))
            {
                flags.Remove(flagName);
                return true;
            }
            return false;
        }

        public void RemoveAllFlags()
        {
            flags.Clear();
        }

        public bool HasFlag(string flagName)
        {
            return flags.Contains(flagName);
        }
    }
}
