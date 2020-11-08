using AllChemist.Properties;
using AllChemist;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using AllChemist.Model;
using AllChemist.GUI.Views;
using AllChemist.GUI.Controllers;
using AllChemist.Cells.Ruleset;
using AllChemist.Cells;

namespace AllChemist
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static readonly string Version = "0.0.4";

        private World model;
        private ControllerContainter controllerInitializer;
        private IRulesetCreator rulesetCreator;
        private MainWindow mainWindow;

        public App()
        {

        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            mainWindow = new MainWindow();
            mainWindow.Show();

            System.Console.WriteLine($"Starting AllChemist {Version}");

            mainWindow.NewGridButton.Click += (s, e) => { CleanUpModel(); InitializeModel(); };
            mainWindow.RulesetLoadButton.Click += (s, e) => {
                rulesetCreator = new FromFileRulesetCreator();
                CleanUpModel();
                InitializeModel();
            };

            controllerInitializer = new ControllerContainter(mainWindow);

            rulesetCreator = new ConwayRulesetCreator("23/3");

            InitializeModel();
        }



        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            Settings.Default.Save();
        }

        private void InitializeModel()
        {
            model = new World(controllerInitializer.GetController<WorldSizeController>().GetWorldSize(), rulesetCreator.CreateRuleset().CellTypeBank);
            controllerInitializer.SetUpModel(model);
            mainWindow.InitializeView(model);
            model.OnWorldChange += mainWindow.WorldView.DeltaDraw;
            mainWindow.WorldView.SetPainter(controllerInitializer.GetController<WorldPainterController>());
            mainWindow.InitializeGUIStateMachine();
        }

        private void CleanUpModel()
        {
            Console.WriteLine("Cleaning up the world...");
            CellType.ResetCounter();
            controllerInitializer.CleanUpModel(model);

            model.OnWorldChange -= mainWindow.WorldView.DeltaDraw;
            mainWindow.WorldView.UnsetPainter(controllerInitializer.GetController<WorldPainterController>());

            GC.Collect();
        }
    }
}
