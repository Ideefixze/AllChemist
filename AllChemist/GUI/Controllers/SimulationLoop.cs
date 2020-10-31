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
public class SimulationLoop
    {
        public bool RunThread = true;
        public bool Simulate = false;
        public int MilisecondsDelay=250;

        public void LoopThread(World w)
        {
            bool DispatcherSync = true;
            while(RunThread)
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