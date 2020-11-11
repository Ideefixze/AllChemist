using System;
using System.Collections.Generic;
using System.Text;

namespace AllChemist.GUI.GUIStates
{
    /// <summary>
    /// GUIStateMachine has a current state and shared state (memory).
    /// </summary>
    public class GUIStateMachine
    {
        public GUIState GUIState;
        public GUISharedState GUISharedState;

        public GUIStateMachine(MainWindow window)
        {
            window.GUIStateMachine?.GUISharedState.RemoveAllFlags();
            GUISharedState = new GUISharedState();
            
        }
    }
}
