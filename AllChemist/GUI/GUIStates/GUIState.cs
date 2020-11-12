using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace AllChemist.GUI.GUIStates
{
    /// <summary>
    /// Base class for all GUIStates that handle GUI behaviour.
    /// </summary>
    public abstract class GUIState
    {
        protected MainWindow mainWindow;

        public GUIState(MainWindow newContext)
        {
            newContext.GUIStateMachine?.GUIState?.CleanUp(); //Clean up previous state (unsubscribe events etc.)
            mainWindow = newContext; //Set a new context
            StateChanged(); //State was changed. Apply all changes for the GUI
        }

        public virtual void CleanUp() { }
        public virtual void StateChanged() { }

    }
}
