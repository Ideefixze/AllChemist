using System;
using System.Collections.Generic;
using System.Text;

namespace AllChemist.GUI.GUIStates
{
    public class GUIStateMachine
    {
        public GUIState GUIState;
        public GUISharedState GUISharedState;

        public GUIStateMachine(MainWindow window)
        {
            window.GUIStateMachine?.GUISharedState.RemoveAllFlags();
            GUISharedState = new GUISharedState();
            GUIState = new IdleGUIState(window);
        }
    }
}
