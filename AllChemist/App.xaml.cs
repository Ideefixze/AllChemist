using AllChemist.Cells;
using AllChemist.Cells.Ruleset;
using AllChemist.GUI.Controllers;
using AllChemist.GUI.Views;
using AllChemist.Model;
using AllChemist.Properties;
using AllChemist.SerializationAndIO;
using System;
using System.Windows;
using System.Windows.Media.Imaging;

namespace AllChemist
{
    /// <summary>
    /// Entry point of our Application.
    /// </summary>
    public partial class App : Application
    {
        public static readonly string Version = "1.0.0";
        public static readonly Random Random = new Random();

        /// <summary>
        /// All important classes for MVC design pattern.
        /// </summary>
        private World model; //Model
        private DefaultControllerContainer controllerContainer; //Controllers - container and initializer
        private RulesetInterpreter rulesetInterpreter; //Controller that generates ruleset (Strategy Pattern)

        private MainWindow mainWindow; //Main GUI container
        private DefaultViewContainer viewContainer; //Views

        public App()
        {

        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            mainWindow = new MainWindow();
            mainWindow.Show();

            System.Console.WriteLine($"Starting AllChemist {Version}");

            //Sets up buttons that generate a new model and/or load a new rulesets 
            mainWindow.NewGridButton.Click += (s, e) => { CleanUpModel(); InitializeModel(); };
            mainWindow.RulesetLoadButton.Click += (s, e) =>
            {
                rulesetInterpreter = new FromFileRulesetCreator();
                rulesetInterpreter.LoadRuleset();
                CleanUpModel();
                InitializeModel();
            };
            mainWindow.ConwayRulesetLoadButton.Click += (s, e) =>
            {
                rulesetInterpreter = new ConwayRulesetCreator("23/3");
                rulesetInterpreter.LoadRuleset();
                CleanUpModel();
                InitializeModel();
            };
            mainWindow.ExportButton.Click += (s, e) =>
            {
                BitmapExporter.Export(viewContainer.GetView<BitmapWorldView>().Bitmap, new PngBitmapEncoder());
            };

            controllerContainer = new DefaultControllerContainer(mainWindow);
            viewContainer = new DefaultViewContainer(mainWindow);

            rulesetInterpreter = new ConwayRulesetCreator("23/3");

            InitializeModel();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            Settings.Default.Save(); //Saves up a Settings (Singleton)
        }

        /// <summary>
        /// Initializes a new model and all objects that use it are updated.
        /// </summary>
        private void InitializeModel()
        {

            //Create a new ruleset from the interpreter
            Ruleset ruleset = rulesetInterpreter.CreateRuleset();
            mainWindow.MetadataTextBlock.Text = $"{ruleset.Name} by {ruleset.AuthorName}";

            //Prepare a new model
            model = new World(controllerContainer.GetController<WorldSizeController>().GetWorldSize(), ruleset.CellTypeBank);

            //Sets up controllers to the newly created model
            controllerContainer.SetUpModel(model);

            model.OnWorldChange += viewContainer.Update;

            viewContainer.GetView<BitmapWorldView>().InitModel(model);
            viewContainer.GetView<BitmapWorldView>().FullDraw(this, new DrawWorldArgs(model));
            viewContainer.Update(this, new DrawWorldArgs(model));

            //Sets up basic painter
            viewContainer.GetView<BitmapWorldView>().SetPainter(controllerContainer.GetController<WorldPainterController>());

            //Sets up GUIStateMachine for different GUI States (State Design Pattern)
            mainWindow.InitializeGUIStateMachine();
        }

        /// <summary>
        /// Cleans up all data that remains after stopping a simmulation.
        /// Prepares enviroment for a new model.
        /// </summary>
        private void CleanUpModel()
        {
            Console.WriteLine("Cleaning up the world...");
            CellType.ResetCounter();
            //Unsets model from all controllers
            controllerContainer.CleanUpModel(model);
            //Unsubscribing so we won't have any memory leaks
            model.OnWorldChange -= viewContainer.Update;
            viewContainer.GetView<BitmapWorldView>().UnsetPainter(controllerContainer.GetController<WorldPainterController>());

            GC.Collect();
        }
    }
}
