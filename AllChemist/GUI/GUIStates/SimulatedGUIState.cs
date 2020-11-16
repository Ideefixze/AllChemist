using System.Windows;

namespace AllChemist.GUI.GUIStates
{
    /// <summary>
    /// Simulated state. Simulation is on and we can't do anything until we stop.
    /// </summary>
    class SimulatedGUIState : GUIState
    {
        private RoutedEventHandler stopSimulation;
        public SimulatedGUIState(MainWindow context) : base(context)
        {

        }

        public override void CleanUp()
        {
            mainWindow.ToggleSimulationButton.Click -= stopSimulation;
        }

        public override void StateChanged()
        {
            stopSimulation = new RoutedEventHandler((s, a) => { mainWindow.GUIStateMachine.GUIState = new IdleGUIState(mainWindow); });

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

            mainWindow.ToggleSimulationButton.Click += stopSimulation;
        }
    }
}
