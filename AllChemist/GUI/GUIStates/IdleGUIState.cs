using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace AllChemist.GUI.GUIStates
{
    class IdleGUIState : GUIState
    {

        private RoutedEventHandler save;
        private RoutedEventHandler runSimulation;

        public IdleGUIState(MainWindow context) : base(context)
        {
            
        }

        public override void EventInitialization()
        {
            save = new RoutedEventHandler((s, a) => { mainWindow.GUIStateMachine.GUISharedState.AddFlag("HasSave"); StateChanged();  });
            runSimulation = new RoutedEventHandler((s, a) => { mainWindow.GUIStateMachine.GUIState = new SimulatedGUIState(mainWindow); });
        }

        public override void CleanUp()
        {
            mainWindow.ToggleSimulationButton.Click -= runSimulation;
            mainWindow.SaveButton.Click -= save;
        }

        public override void StateChanged()
        {
            mainWindow.ToggleSimulationButton.Content = "Run";
            mainWindow.ToggleSimulationButton.IsEnabled = true;
            mainWindow.SaveButton.IsEnabled = true;
            mainWindow.LoadButton.IsEnabled = mainWindow.GUIStateMachine.GUISharedState.HasFlag("HasSave");//(mainWindow.GUIStateMachine!=null?mainWindow.GUIStateMachine.GUISharedState.HasFlag("HasSave"):false); //Quick fix, because IdleGUIState is always the first state
            mainWindow.RulesetLoadButton.IsEnabled = true;
            mainWindow.CellColorPicker.IsEnabled = true;
            mainWindow.NewGridButton.IsEnabled = true;
            mainWindow.DelayTextBox.IsEnabled = true;
            mainWindow.SizeXTextBox.IsEnabled = true;
            mainWindow.SizeYTextBox.IsEnabled = true;

            mainWindow.ToggleSimulationButton.Click += runSimulation;
            mainWindow.SaveButton.Click += save;
        }
    }
}
