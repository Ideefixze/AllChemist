using AllChemist.Cells;
using AllChemist.Cells.Ruleset;
using AllChemist.GUI.Controllers;
using AllChemist.GUI.GUIStates;
using AllChemist.GUI.Views;
using AllChemist.Model;
using AllChemist.Serialization;
using System;
using System.Windows;

namespace AllChemist
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        World model;
        WorldViewBitmap worldView;

        public GUIStateMachine GUIStateMachine;

        private ModelSimulationController modelSimulationController;
        private WorldPainterController cellPainterController;
        private SnapshotController snapshotController;
        private WorldSizeController worldSizeController;

        private IRulesetCreator rulesetCreator;

        public void InitializeWorld()
        {
            Console.WriteLine("Initializing a new world...");

            CellTypeBank ctb = rulesetCreator.CreateRuleset().CellTypeBank;
            Console.WriteLine(JSONHandler.Save(ctb));
            model = new World(worldSizeController.GetWorldSize(), ctb);

            //Initializing view
            worldView = new WorldViewBitmap(WorldGrid, new Vector2Int(600,600));
            worldView.InitModel(model);
            model.OnWorldChange += worldView.DeltaDraw;
            worldView.FullDraw(this, new DrawWorldArgs(model));

            //Initializing Controllers
            modelSimulationController.InitializeWorldSimulationThread(model);

            cellPainterController = new WorldPainterController(CellColorPicker, model);

            worldView.Image.MouseLeftButtonDown += cellPainterController.StartPainting;
            worldView.Image.MouseLeftButtonUp += cellPainterController.StopPainting;
            worldView.Image.MouseLeave += cellPainterController.StopPainting;

            snapshotController.SetSource(model);

            GUIStateMachine = new GUIStateMachine(this);
            GUIStateMachine.GUIState = new IdleGUIState(this);
           
        }

        public void CleanUpWorld()
        {
            Console.WriteLine("Cleaning up the world...");
            CellType.ResetCounter();
            modelSimulationController?.Dispose();

            model.OnWorldChange -= worldView.DeltaDraw;
            worldView.Image.MouseLeftButtonDown -= cellPainterController.StartPainting;
            worldView.Image.MouseLeftButtonUp -= cellPainterController.StopPainting;
            worldView.Image.MouseLeave -= cellPainterController.StopPainting;

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

            rulesetCreator = new ConwayRulesetCreator("23/3");

            //cellTypeBankFileLoader = new JSONFileDeserializer<CellTypeBank>("rulesets","default.json");
            //cellTypeBankFileLoader.fileDialog.FileOk += (s, e) => { CleanUpWorld(); InitializeWorld(); };
            //RulesetLoadButton.Click += (s,e) => { cellTypeBankFileLoader.ShowOpenFileDialog(); };

            InitializeWorld();
        }



    }


}
