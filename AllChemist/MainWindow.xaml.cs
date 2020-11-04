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
        WorldViewCanvas worldView;

        public GUIStateMachine GUIStateMachine;

        private ModelSimulationController modelSimulationController;
        private WorldPainterController cellPainterController;
        private SnapshotController snapshotController;
        private WorldSizeController worldSizeController;

        private IRulesetCreator rulesetCreator;

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

            CellTypeBank ctb = rulesetCreator.CreateRuleset().CellTypeBank;
            Console.WriteLine(JSONHandler.Save(ctb));
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

            GUIStateMachine = new GUIStateMachine(this);
            GUIStateMachine.GUIState = new IdleGUIState(this);
           
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

            rulesetCreator = new ConwayRulesetCreator("23/3");

            //cellTypeBankFileLoader = new JSONFileDeserializer<CellTypeBank>("rulesets","default.json");
            //cellTypeBankFileLoader.fileDialog.FileOk += (s, e) => { CleanUpWorld(); InitializeWorld(); };
            //RulesetLoadButton.Click += (s,e) => { cellTypeBankFileLoader.ShowOpenFileDialog(); };

            InitializeWorld();
        }



    }


}
