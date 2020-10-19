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

namespace AllChemist
{
    public class WorldViewCanvas
    {
        private Canvas canvas;
        private Rectangle[,] rectangles;

        public Point MousePointRelativeToCanvas()
        {
            return Mouse.GetPosition(canvas);
        }

        public WorldViewCanvas(Grid main, int windowSizeX, int windowSizeY)
        {
            canvas = new Canvas();
            canvas.Width = windowSizeX;
            canvas.Height = windowSizeY;

            canvas.AddHandler(Mouse.MouseDownEvent, new RoutedEventHandler(Handle));

            main.Children.Add(canvas);
        }

        public void InitCanvasRects(World w)
        {
            rectangles = new Rectangle[w.CurrentTable.Size.x, w.CurrentTable.Size.y];
            for (int i = 0; i < w.CurrentTable.Size.x; i++)
            {
                for (int j = 0; j < w.CurrentTable.Size.y; j++)
                {
                    Rectangle rect = new Rectangle();
                    rect.Width = canvas.Width/w.CurrentTable.Size.x;
                    rect.Height = canvas.Height/w.CurrentTable.Size.y;

                    SolidColorBrush solidColorBrush = new SolidColorBrush();
                    solidColorBrush.Color = w.CurrentTable.Cells[i, j].CellType.color;
                    rect.Fill = solidColorBrush;
                    rect.Stroke = Brushes.Black;
                    rect.ToolTip = $"{w.CurrentTable.Cells[i, j].CellType.name} ({w.CurrentTable.Cells[i, j].CellType.id})\nAt position ({j},{w.CurrentTable.Size.x - i - 1})";
                    canvas.Children.Add(rect);

                    Canvas.SetLeft(rect, i * canvas.Width / w.CurrentTable.Size.x);
                    Canvas.SetTop(rect, j * canvas.Height / w.CurrentTable.Size.y);

                    rectangles[i, j] = rect;
                }
            }
        }

        public void Handle(object sender, RoutedEventArgs args)
        {
            
            System.Console.WriteLine(Mouse.GetPosition(canvas));
        }

        public void OnMouseOverCell(object sender, RoutedEventArgs args)
        {

        }
        public void Draw(object sender, OnWorldStepArgs args)
        {
            canvas.Dispatcher.Invoke(() => { 
                for (int i = 0; i < args.World.CurrentTable.Size.x; i++)
                {
                    for (int j = 0; j < args.World.CurrentTable.Size.y; j++)
                    {

                        SolidColorBrush solidColorBrush = new SolidColorBrush();
                        solidColorBrush.Color = args.World.CurrentTable.Cells[i, j].CellType.color;
                        rectangles[i,j].Fill = solidColorBrush;

                        rectangles[i, j].ToolTip = $"{args.World.CurrentTable.Cells[i, j].CellType.name} ({args.World.CurrentTable.Cells[i, j].CellType.id})\nAt position ({j},{args.World.CurrentTable.Size.x-i-1})";
                    }
                
                }
            });


        }
    }
    
    public class SimulationLoop
    {
       public bool Simulate = false;

        public void LoopThread(World w, int milisecondsDelay)
        {
            while(true)
            {
                if (Simulate)
                {
                    w?.Step();
                    Simulate = false;
                    Application.Current.Dispatcher.Invoke(() => { Simulate = true; });
                    Thread.Sleep(milisecondsDelay);
                }
                Thread.Sleep(50);
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
    /// Controler that handles running simulation loop
    /// </summary>
    public class ModelSimulationController
    {
        private SimulationLoop simulationLoop;
        private Button toggleButton;

        public ModelSimulationController(Button originalButton, World w)
        {
            toggleButton = originalButton;
            toggleButton.IsEnabled = true;
            toggleButton.Click += ToggleSimulation;
            simulationLoop = new SimulationLoop();

            Thread simulationThread = new Thread(() => { simulationLoop.LoopThread(w, 250); });
            simulationThread.IsBackground = true;
            simulationThread.Start();
        }

        public void ToggleSimulation(object sender, RoutedEventArgs args)
        {
            simulationLoop.Simulate = !simulationLoop.Simulate;
            toggleButton.Content = simulationLoop.Simulate ? "Stop" : "Run";
        }
    }


    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ModelSimulationController modelSimulationController;

        public MainWindow()
        {
            InitializeComponent();
            this.Title = "AllChemist " + App.Version + "v";

            CellType typeA = new CellType("Red",255,0,0);
            CellType typeB = new CellType("Green",0,255,0);

            typeA.CellBehaviour.Rules.Add(new SwapToRule(typeB));
            typeB.CellBehaviour.Rules.Add(new SwapToRule(typeA));

            World w = new World(new Vector2Int(100, 100), typeA);
            WorldViewCanvas wv = new WorldViewCanvas(WorldGrid, 600, 600);
            wv.InitCanvasRects(w);
            w.OnWorldStep += wv.Draw;
            wv.Draw(this, new OnWorldStepArgs(w));

            modelSimulationController = new ModelSimulationController(ToggleSimulationButton, w);
        }

    }


}
