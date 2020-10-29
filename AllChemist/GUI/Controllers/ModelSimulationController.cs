using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Text.RegularExpressions;
using Xceed.Wpf;
using Xceed.Wpf.Toolkit;

namespace AllChemist
{
/// <summary>
    /// Controler that handles toggling simulation loop
    /// </summary>
    public class ModelSimulationController : IDisposable
    {
        private SimulationLoop simulationLoop;
        private Button toggleButton;
        private TextBox delayTextBox;

        public ModelSimulationController(Button originalButton, TextBox delayTextBox)
        {
            this.delayTextBox = delayTextBox; 
            toggleButton = originalButton;
            toggleButton.IsEnabled = true;
            toggleButton.Click += ToggleSimulation;

            DisplayButton();
        }

        public void InitializeWorldSimulationThread(World w)
        {
            simulationLoop = new SimulationLoop();
            
            Thread simulationThread = new Thread(() => { simulationLoop.LoopThread(w); });
            simulationThread.IsBackground = true;
            simulationThread.Name = "Simulation";
            simulationThread.Start();

            DisplayButton();
        }

        public void ToggleSimulation(object sender, RoutedEventArgs args)
        {
            simulationLoop.Simulate = !simulationLoop.Simulate;

            DisplayButton();

            simulationLoop.MilisecondsDelay = Int32.Parse(delayTextBox.Text); 
        }

        /// <summary>
        /// Modifies button content to look correctly 
        /// </summary>
        public void DisplayButton()
        {
            if(simulationLoop!=null)
            {
                toggleButton.Content = simulationLoop.Simulate ? "Stop" : "Run";
                delayTextBox.IsEnabled = !simulationLoop.Simulate;
            }
            
        }

        public void Dispose()
        {            
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                simulationLoop.RunThread = false;
            }
            
        }
    }
}