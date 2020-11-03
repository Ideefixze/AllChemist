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

        public bool IsCurrentlySimulated { get => simulationLoop.Simulate; }
        public ModelSimulationController(Button originalButton, TextBox delayTextBox)
        {
            this.delayTextBox = delayTextBox;
            this.delayTextBox.PreviewTextInput += TextValidator.NumberValidationTextBox;
            toggleButton = originalButton;
            toggleButton.IsEnabled = true;
            toggleButton.Click += ToggleSimulation;
        }

        public void InitializeWorldSimulationThread(World w)
        {
            simulationLoop = new SimulationLoop();
            
            Thread simulationThread = new Thread(() => { simulationLoop.LoopThread(w); });
            simulationThread.IsBackground = true;
            simulationThread.Name = "Simulation";
            simulationThread.Start();
        }

        public void ToggleSimulation(object sender, RoutedEventArgs args)
        {
            simulationLoop.Simulate = !simulationLoop.Simulate;
            simulationLoop.MilisecondsDelay = Int32.Parse(delayTextBox.Text); 
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