using AllChemist.Model;
using System.Diagnostics;
using System.Threading;
using System.Windows;

namespace AllChemist.GUI.Controllers
{
    /// <summary>
    /// Handles a thread that runs for model simulation.
    /// </summary>
    public class SimulationLoop
    {
        public bool RunThread = true;
        public bool Simulate = false;
        public int MilisecondsDelay = 250;

        public void LoopThread(World w)
        {
            //DispatcherSync synchronizes loop with a GUI so that each step is displayed
            bool DispatcherSync = true;
            Stopwatch timer = new Stopwatch();
            while (RunThread)
            {
                if (Simulate && DispatcherSync)
                {
                    w?.Step();
                    DispatcherSync = false;
                    Application.Current.Dispatcher.Invoke(() => { DispatcherSync = true; });
                    Thread.Sleep(MilisecondsDelay);
                }
                Thread.Sleep(5);
            }
        }
    }
}