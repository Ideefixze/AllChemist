using AllChemist.Model;
using System.Windows;

namespace AllChemist.GUI.Controllers
{
    /// <summary>
    /// Holds a temporary snapshot of the model so it can be restored later.
    /// </summary>
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
            if (snapshot == null)
            {
                System.Windows.MessageBox.Show("Create a snapshot first.", "AllChemist", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
                world.RestoreSnapshot(snapshot);
        }
    }
}