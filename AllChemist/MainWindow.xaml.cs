﻿using System;
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

namespace AllChemist
{
    public class WorldViewCanvas
    {
        public Canvas Canvas;

        private Rectangle[,] rectangles;

        public Point MousePointRelativeToCanvas()
        {
            return Mouse.GetPosition(Canvas);
        }

        public WorldViewCanvas(Grid main, int windowSizeX, int windowSizeY)
        {
            Canvas = new Canvas();
            Canvas.Width = windowSizeX;
            Canvas.Height = windowSizeY;

            main.Children.Add(Canvas);
        }

        public void InitCanvasRects(World w)
        {
            rectangles = new Rectangle[w.CurrentTable.Size.X, w.CurrentTable.Size.Y];
            for (int i = 0; i < w.CurrentTable.Size.X; i++)
            {
                for (int j = 0; j < w.CurrentTable.Size.Y; j++)
                {
                    Rectangle rect = new Rectangle();
                    rect.Width = Canvas.Width/w.CurrentTable.Size.X;
                    rect.Height = Canvas.Height/w.CurrentTable.Size.Y;

                    SolidColorBrush solidColorBrush = new SolidColorBrush();
                    solidColorBrush.Color = w.CurrentTable.Cells[i, j].CellType.color;
                    rect.Fill = solidColorBrush;
                    rect.Stroke = Brushes.Black;
                    rect.ToolTip = $"{w.CurrentTable.Cells[i, j].CellType.name} ({w.CurrentTable.Cells[i, j].CellType.id})\nAt position ({j},{w.CurrentTable.Size.X - i - 1})";
                    Canvas.Children.Add(rect);

                    Canvas.SetLeft(rect, i * Canvas.Width / w.CurrentTable.Size.X);
                    Canvas.SetTop(rect, j * Canvas.Height / w.CurrentTable.Size.Y);

                    rectangles[i, j] = rect;
                }
            }
        }

        public void Draw(object sender, OnWorldChangeArgs args)
        {
            Canvas.Dispatcher.Invoke(() => { 
                for (int i = 0; i < args.World.CurrentTable.Size.X; i++)
                {
                    for (int j = 0; j < args.World.CurrentTable.Size.Y; j++)
                    {

                        SolidColorBrush solidColorBrush = new SolidColorBrush();
                        solidColorBrush.Color = args.World.CurrentTable.Cells[i, j].CellType.color;
                        rectangles[i,j].Fill = solidColorBrush;

                        rectangles[i, j].ToolTip = $"{args.World.CurrentTable.Cells[i, j].CellType.name} ({args.World.CurrentTable.Cells[i, j].CellType.id})\nAt position ({j},{args.World.CurrentTable.Size.X-i-1})";
                    }
                
                }
            });


        }
    }
    
    public class SimulationLoop
    {
        public bool Simulate = false;
        public int MilisecondsDelay=250;

        public void LoopThread(World w)
        {
            while(true)
            {
                if (Simulate)
                {
                    w?.Step();
                    Simulate = false;
                    Application.Current.Dispatcher.Invoke(() => { Simulate = true; });
                    Thread.Sleep(MilisecondsDelay);
                }
                Thread.Sleep(10);
            }
        }

        public void LoopThread2(World w, int milisecondsDelay)
        {
            while (true)
            {
                if (Simulate)
                {
                    w?.Step();
                    Thread.Sleep(milisecondsDelay); //There is no way to wait for Render Thread thus we have to step
                }
                Thread.Sleep(100);
            }
        }
    }

    /// <summary>
    /// Controler that handles toggling simulation loop
    /// </summary>
    public class ModelSimulationController
    {
        private SimulationLoop simulationLoop;
        private Button toggleButton;
        private TextBox delayTextBox;

        public ModelSimulationController(Button originalButton, TextBox delayTextBox, World w)
        {
            this.delayTextBox = delayTextBox; 
            toggleButton = originalButton;
            toggleButton.IsEnabled = true;
            toggleButton.Click += ToggleSimulation;
            simulationLoop = new SimulationLoop();
            

            Thread simulationThread = new Thread(() => { simulationLoop.LoopThread(w); });
            simulationThread.IsBackground = true;
            simulationThread.Start();
        }

        public void ToggleSimulation(object sender, RoutedEventArgs args)
        {
            simulationLoop.Simulate = !simulationLoop.Simulate;

            toggleButton.Content = simulationLoop.Simulate ? "Stop" : "Run";
            delayTextBox.IsEnabled = !simulationLoop.Simulate;

            simulationLoop.MilisecondsDelay = Int32.Parse(delayTextBox.Text); 
        }
    }

    public class CellPainterController
    {
        private World world;
        private ColorPicker colorPicker;
        private Thread paintingThread;
        private bool isPainting;
        public CellPainterController(ColorPicker cp, World w)
        {
            world = w;
            colorPicker = cp;
            colorPicker.ShowRecentColors = false;
            colorPicker.ShowStandardColors = false;
            colorPicker.ShowTabHeaders = false;
            colorPicker.AvailableColors.Clear();
            foreach(KeyValuePair<int, CellType> kv in world.CellTypeBank.CellTypes)
            {
                colorPicker.AvailableColors.Add(new ColorItem(kv.Value.color, kv.Value.name));
            }
            colorPicker.SelectedColor = colorPicker.AvailableColors[0].Color;
            
        }

        public CellType GetCurrentCellType(CellTypeBank cellTypeBank)
        {
            //WARNING: No duplicate colors in CellTypeBank please
            return cellTypeBank.CellTypes.FirstOrDefault(t => t.Value.color == colorPicker.SelectedColor).Value;
        }

        public void StartPainting(object sender, RoutedEventArgs args)
        {
            isPainting = true;
            paintingThread = new Thread(() => { PaintWorld((Canvas)sender); });
            paintingThread.Start();
        }

        public void StopPainting(object sender, RoutedEventArgs args)
        {
            isPainting = false;
            world.ApplyChanges();
        }

        public void PaintWorld(Canvas canvas)
        {
            while(isPainting)
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    Point mousePoint = Mouse.GetPosition(canvas);
                    Vector2Int worldPos = new Vector2Int((int)mousePoint.X / ((int)canvas.Width / world.TableSize.X), (int)mousePoint.Y / ((int)canvas.Height / world.TableSize.Y));
                    this.PaintWorld(worldPos); 
                });
                Thread.Sleep(20);
            }
            
        }

        private void PaintWorld(Vector2Int pos)
        {
             world.Paint(pos, GetCurrentCellType(world.CellTypeBank));
        }
    }

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ModelSimulationController modelSimulationController;
        private CellPainterController cellPainterController;

        public MainWindow()
        {
            InitializeComponent();
            this.Title = "AllChemist " + App.Version + "v";


            //Initializing Cell Types and World
            /*
            CellType typeA = new CellType("Red",255,0,0);
            CellType typeB = new CellType("Green",0,255,0);

            typeA.CellBehaviour.Rules.Add(new SwapToRule(1));
            typeB.CellBehaviour.Rules.Add(new SwapToRule(0));

            CellTypeBank ctb = new CellTypeBank();
            ctb.CellTypes.Add(typeA.id, typeA);
            ctb.CellTypes.Add(typeB.id, typeB);

            ctb.LoadFromJson(ctb.SaveToJson());
            */
            //Game of Life
            CellType deadType = new CellType("Dead", 255, 255, 255);
            CellType aliveType = new CellType("Alive", 0, 0, 0);

            CellTypeBank ctb = new CellTypeBank();
            ctb.CellTypes.Add(deadType.id, deadType);
            ctb.CellTypes.Add(aliveType.id, aliveType);

            deadType.CellBehaviour.Rules.Add(new NeighbourChangeTo(1, 3));

            aliveType.CellBehaviour.Rules.Add(new SwapToRule(0));
            aliveType.CellBehaviour.Rules.Add(new NeighbourChangeTo(1, 2));
            aliveType.CellBehaviour.Rules.Add(new NeighbourChangeTo(1, 3));

            World w = new World(new Vector2Int(50, 50), ctb);

            //Initializing view
            WorldViewCanvas wv = new WorldViewCanvas(WorldGrid, 600, 600);
            wv.InitCanvasRects(w);
            w.OnWorldChange += wv.Draw;
            wv.Draw(this, new OnWorldChangeArgs(w));

            //Initializing Controllers
            modelSimulationController = new ModelSimulationController(ToggleSimulationButton, DelayTextBox, w);
            cellPainterController = new CellPainterController(CellColorPicker, w);
            wv.Canvas.MouseLeftButtonDown += cellPainterController.StartPainting;
            wv.Canvas.MouseLeftButtonUp += cellPainterController.StopPainting;
            wv.Canvas.MouseLeave += cellPainterController.StopPainting;

        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

    }


}
