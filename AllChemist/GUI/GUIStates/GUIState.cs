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

        public GUIState(MainWindow context)
        {
            context.GUIStateMachine?.GUIState?.CleanUp();
            mainWindow = context;
            EventInitialization();
            StateChanged();
        }

        public virtual void EventInitialization() { }
        public virtual void CleanUp() { }
        public virtual void StateChanged() { }

    }
}
