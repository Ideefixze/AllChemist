using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace AllChemist.GUI.GUIStates
{
    class SimulatedGUIState : GUIState
    {
        private RoutedEventHandler stopSimulation;
        public SimulatedGUIState(MainWindow context) : base(context)
        {
            
        }

        public override void EventInitialization()
        {
            stopSimulation = new RoutedEventHandler((s, a) => { mainWindow.GUIStateMachine.GUIState = new IdleGUIState(mainWindow); });
        }

        public override void CleanUp()
        {
            mainWindow.ToggleSimulationButton.Click -= stopSimulation;
        }

        public override void StateChanged() 
        {
            mainWindow.ToggleSimulationButton.Content = "Stop";
            mainWindow.ToggleSimulationButton.IsEnabled = true;
            mainWindow.SaveButton.IsEnabled = false;
            mainWindow.LoadButton.IsEnabled = false;
            mainWindow.RulesetLoadButton.IsEnabled = false;
            mainWindow.ConwayRulesetLoadButton.IsEnabled = false;
            mainWindow.CellColorPicker.IsEnabled = true;
            mainWindow.NewGridButton.IsEnabled = false;
            mainWindow.DelayTextBox.IsEnabled = false;
            mainWindow.SizeXTextBox.IsEnabled = false;
            mainWindow.SizeYTextBox.IsEnabled = false;
            
            //TODO: Show number of steps

            mainWindow.ToggleSimulationButton.Click += stopSimulation;
        }
    }
}
