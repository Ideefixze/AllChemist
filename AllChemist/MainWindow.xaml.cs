﻿using AllChemist.Cells;
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

        public GUIStateMachine GUIStateMachine;

        public void InitializeGUIStateMachine()
        {
            GUIStateMachine = new GUIStateMachine(this);
            GUIStateMachine.GUIState = new IdleGUIState(this);
        }

        public MainWindow()
        {
            InitializeComponent();
            this.Title = $"AllChemist {App.Version } v";
        }

    }


}
