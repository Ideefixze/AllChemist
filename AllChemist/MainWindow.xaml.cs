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
using AllChemist.GUI.Controllers;

namespace AllChemist
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        World model;
        WorldViewCanvas worldView;

        private ModelSimulationController modelSimulationController;
        private WorldPainterController cellPainterController;
        private SnapshotController snapshotController;
        private WorldSizeController worldSizeController;
        private JSONFileDeserializer<CellTypeBank> cellTypeBankFileLoader;

        public void InitializeWorld()
        {

            //Game of Life
            /*CellType deadType = new CellType("Dead", 255, 255, 255);
            CellType aliveType = new CellType("Alive", 0, 0, 0);
            CellTypeBank ctb = new CellTypeBank();
            ctb.CellTypes.Add(deadType.id, deadType);
            ctb.CellTypes.Add(aliveType.id, aliveType);
            deadType.CellBehaviour.Rules.Add(new NeighbourChangeTo(1, 3));
            aliveType.CellBehaviour.Rules.Add(new SwapToRule(0));
            aliveType.CellBehaviour.Rules.Add(new NeighbourChangeTo(1, 2));
            aliveType.CellBehaviour.Rules.Add(new NeighbourChangeTo(1, 3));
            */
            Console.WriteLine("Initializing a new world...");

            CellTypeBank ctb = cellTypeBankFileLoader.GetData();
            model = new World(worldSizeController.GetWorldSize(), ctb);

            //Initializing view
            worldView = new WorldViewCanvas(WorldGrid, 600, 600);
            worldView.InitCanvasRects(model);
            model.OnWorldChange += worldView.Draw;
            worldView.Draw(this, new DrawWorldArgs(model));

            //Initializing Controllers
            modelSimulationController.InitializeWorldSimulationThread(model);

            cellPainterController = new WorldPainterController(CellColorPicker, model);

            worldView.Canvas.MouseLeftButtonDown += cellPainterController.StartPainting;
            worldView.Canvas.MouseLeftButtonUp += cellPainterController.StopPainting;
            worldView.Canvas.MouseLeave += cellPainterController.StopPainting;

            snapshotController.SetSource(model);
        }

        public void CleanUpWorld()
        {
            Console.WriteLine("Cleaning up the world...");
            CellType.ResetCounter();
            modelSimulationController?.Dispose();

            model.OnWorldChange -= worldView.Draw;
            worldView.Canvas.MouseLeftButtonDown -= cellPainterController.StartPainting;
            worldView.Canvas.MouseLeftButtonUp -= cellPainterController.StopPainting;
            worldView.Canvas.MouseLeave -= cellPainterController.StopPainting;

            GC.Collect();
        }

        public MainWindow()
        {
            InitializeComponent();
            this.Title = "AllChemist " + App.Version + "v";

            modelSimulationController = new ModelSimulationController(ToggleSimulationButton, DelayTextBox);

            snapshotController = new SnapshotController(SaveButton, LoadButton);

            worldSizeController = new WorldSizeController(SizeXTextBox, SizeYTextBox);
            NewGridButton.Click += (s,e) => { CleanUpWorld(); InitializeWorld(); };

            cellTypeBankFileLoader = new JSONFileDeserializer<CellTypeBank>("rulesets","default.json");
            cellTypeBankFileLoader.fileDialog.FileOk += (s, e) => { CleanUpWorld(); InitializeWorld(); };
            RulesetLoadButton.Click += (s,e) => { cellTypeBankFileLoader.ShowOpenFileDialog(); };

            InitializeWorld();
        }



    }


}
