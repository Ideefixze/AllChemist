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
    /// Main Window: used primarly as a container of all GUI elements and as a View wrapper.
    /// </summary>
    public partial class MainWindow : Window
    {
        public WorldViewBitmap WorldView;
        public WorldViewSteps WorldViewSteps;

        public GUIStateMachine GUIStateMachine;

        public void InitializeGUIStateMachine()
        {
            GUIStateMachine = new GUIStateMachine(this);
            GUIStateMachine.GUIState = new IdleGUIState(this);
        }

        public void InitializeView(World model)
        {
            WorldView = new WorldViewBitmap(WorldGrid, new Vector2Int(600, 600));
            WorldView.InitModel(model);
            
            WorldView.FullDraw(this, new DrawWorldArgs(model));

            WorldViewSteps = new WorldViewSteps(StepsTextBlock);
        }

        public MainWindow()
        {
            InitializeComponent();
            this.Title = $"AllChemist {App.Version } v";
            
        }

    }


}
