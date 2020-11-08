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
using AllChemist.Model;

namespace AllChemist.GUI.Controllers
{
public class SnapshotController : GUIContextController
    {
        private World world;
        private CellTable snapshot;

        public SnapshotController(MainWindow context) : base(context)
        {
            context.SaveButton.Click += SaveWorld;
            context.LoadButton.Click += LoadWorld;
        }

        public override void SetUpModel(World w)
        {
            world = w;
            snapshot = null; 
        }


        private void SaveWorld(object sender, RoutedEventArgs e)
        {
            snapshot = world.CreateSnapshot();
        }

        private void LoadWorld(object sender, RoutedEventArgs e)
        {
            if(snapshot==null)
            {
                System.Windows.MessageBox.Show("Create a snapshot first.", "AllChemist", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
                world.RestoreSnapshot(snapshot);
        }
    }
}