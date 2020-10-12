using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace AllChemist
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static readonly string Version = "0.0.1";
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            //TODO: Something for better logging etc.
            System.Console.WriteLine($"Starting AllChemist {Version}");
        }
    }
}
