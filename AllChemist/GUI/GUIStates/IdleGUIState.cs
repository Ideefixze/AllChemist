﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace AllChemist.GUI.GUIStates
{
    /// <summary>
    /// Idle state. Used when simulation is off.
    /// </summary>
    class IdleGUIState : GUIState
    {

        private RoutedEventHandler save;
        private RoutedEventHandler runSimulation;

        public IdleGUIState(MainWindow context) : base(context)
        {
            
        }

        public override void CleanUp()
        {
            mainWindow.ToggleSimulationButton.Click -= runSimulation;
            mainWindow.SaveButton.Click -= save;
        }

        public override void StateChanged()
        {
            save = new RoutedEventHandler((s, a) => { mainWindow.GUIStateMachine.GUISharedState.AddFlag("HasSave"); StateChanged(); });
            runSimulation = new RoutedEventHandler((s, a) => { mainWindow.GUIStateMachine.GUIState = new SimulatedGUIState(mainWindow); });

            mainWindow.ToggleSimulationButton.Content = "Run";

            mainWindow.ToggleSimulationButton.IsEnabled = true;
            mainWindow.DelayTextBox.Text = Properties.Settings.Default.Delay;

            mainWindow.SaveButton.IsEnabled = true;
            mainWindow.LoadButton.IsEnabled = mainWindow.GUIStateMachine.GUISharedState.HasFlag("HasSave");
            mainWindow.RulesetLoadButton.IsEnabled = true;
            mainWindow.ConwayRulesetLoadButton.IsEnabled = true;
            mainWindow.CellColorPicker.IsEnabled = true;
            mainWindow.NewGridButton.IsEnabled = true;
            mainWindow.DelayTextBox.IsEnabled = true;

            mainWindow.SizeXTextBox.IsEnabled = true;
            mainWindow.SizeXTextBox.Text = Properties.Settings.Default.SizeX;
            mainWindow.SizeYTextBox.IsEnabled = true;
            mainWindow.SizeYTextBox.Text = Properties.Settings.Default.SizeY;

            mainWindow.ToggleSimulationButton.Click += runSimulation;
            mainWindow.SaveButton.Click += save;
        }
    }
}
