using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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

        public WorldViewCanvas(Grid main, int windowSizeX, int windowSizeY)
        {
            canvas = new Canvas();
            canvas.Width = windowSizeX;
            canvas.Height = windowSizeY;

            main.Children.Add(canvas);
        }

        public void Draw(object sender, OnWorldStepArgs args)
        {
            canvas.Dispatcher.Invoke(() => { 
            canvas.Children.Clear();
            for (int i = 0; i < args.World.CurrentTable.Size.x; i++)
            {
                for (int j = 0; j < args.World.CurrentTable.Size.y; j++)
                {

                    Rectangle rect = new Rectangle();
                    rect.Width = canvas.Width/ args.World.CurrentTable.Size.x;
                    rect.Height = canvas.Height / args.World.CurrentTable.Size.y;
                    
                    SolidColorBrush solidColorBrush = new SolidColorBrush();
                    solidColorBrush.Color = Color.FromRgb((byte)(i*10), (byte)(j * 10), 0);
                    rect.Fill = solidColorBrush;
                    canvas.Children.Add(rect);
                    Canvas.SetLeft(rect, j*canvas.Width / args.World.CurrentTable.Size.x);
                    Canvas.SetTop(rect, i*canvas.Height / args.World.CurrentTable.Size.y);
                }
                
                
            }
            });


        }
    }
    public class WorldView
    {

        private Emoji.Wpf.TextBlock textBlock;

        public WorldView(Grid main, int windowSizeX, int windowSizeY)
        {
            textBlock = new Emoji.Wpf.TextBlock();
            textBlock.Text = "?";
            textBlock.FontSize = 16;
            textBlock.TextAlignment = TextAlignment.Center;
            textBlock.MaxHeight = windowSizeY;
            textBlock.MaxWidth = windowSizeX;
            textBlock.Background = Brushes.White;

            main.Children.Add(textBlock);
        }
        public void Draw(object sender, OnWorldStepArgs args)
        {
            string display = "";
            for (int i = 0; i < args.World.CurrentTable.Size.x; i++)
            {
                for (int j = 0; j < args.World.CurrentTable.Size.y; j++)
                {
                    display += args.World.CurrentTable.Cells[i, j].CellType.art;
                }
                display += "\n";
            }

            textBlock.Dispatcher.Invoke(() => { textBlock.Text = display; });
        }
    }
    public class WorldGridView
    {

        private Vector2Int size;
        private Grid mainGrid;
        private Emoji.Wpf.TextBlock[,] textBlocks;

        public WorldGridView(Vector2Int size, Grid mainGrid)
        {
            this.size = size;
            this.mainGrid = mainGrid;
        }
        public void CreateWorldGrid(int windowSizeX = 600, int windowSizeY = 600)
        {
            Grid worldGrid = new Grid();
            worldGrid.Width = windowSizeX;
            worldGrid.Height = windowSizeY;
            worldGrid.HorizontalAlignment = HorizontalAlignment.Left;
            worldGrid.VerticalAlignment = VerticalAlignment.Center;
            worldGrid.ShowGridLines = true;

            textBlocks = new Emoji.Wpf.TextBlock[size.x, size.y];


            for (int i = 0; i < size.x; i++)
            {
                ColumnDefinition cd = new ColumnDefinition();
                cd.MaxWidth = windowSizeX / size.x;
                worldGrid.ColumnDefinitions.Add(cd);
            }

            for (int i = 0; i < size.y; i++)
            {
                RowDefinition rd = new RowDefinition();
                rd.MaxHeight = windowSizeY / size.y;
                worldGrid.RowDefinitions.Add(rd);
            }

            for (int i = 0; i < size.x; i++)
            {
                for (int j = 0; j < size.y; j++)
                {
                    Grid g = new Grid();
                    g.HorizontalAlignment = HorizontalAlignment.Stretch;
                    g.VerticalAlignment = VerticalAlignment.Center;

                    Emoji.Wpf.TextBlock textBlock = new Emoji.Wpf.TextBlock();
                    textBlocks[i, j] = textBlock;
                    textBlock.Text = "?";
                    textBlock.FontSize = 16;
                    textBlock.TextAlignment = TextAlignment.Center;
                    textBlock.MaxHeight = windowSizeY / size.y;
                    textBlock.MaxWidth = windowSizeX / size.x;

                    g.Children.Add(textBlock);

                    Grid.SetRow(g, j);
                    Grid.SetColumn(g, i);
                    worldGrid.Children.Add(g);
                }
            }

            mainGrid.Children.Add(worldGrid);
        }

        public void Draw(object sender, OnWorldStepArgs args)
        {
            if(args.World.TableSize!=size)
            {
                System.Console.WriteLine("World size and WorldGrid size doesn't match!");
                return;
            }
            
            for (int i = 0; i < size.x; i++)
            {
                for (int j = 0; j < size.y; j++)
                {
                    Console.WriteLine(args.World.CurrentTable.Cells[i, j].CellType.art);
                    //textBlocks[i, j].Dispatcher.Invoke(() => { textBlocks[i, j].Text = args.World.CurrentTable.Cells[i, j].CellType.art; },DispatcherPriority.Render);
                }
            }
        }
    }

    public class Loops
    {
        public static void Loop(World w)
        {
            w?.Step();
        }

        public static void LoopThread(World w)
        {
            while(true)
            {
                w?.Step();
                Thread.Sleep(1000);
            }
        }
    }


    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.Title = "AllChemist " + App.Version + "v";
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            CellType typeA = new CellType("😁");
            CellType typeB = new CellType("❤️");
            CellType typeC = new CellType("❤️");

            typeA.CellBehaviour.Rules.Add(new SwapToRule(typeB));
            typeB.CellBehaviour.Rules.Add(new SwapToRule(typeA));

            World w = new World(new Vector2Int(40, 40), typeA, typeC);
            //WorldGridView wgv = new WorldGridView(new Vector2Int(20, 20), MainGrid);
            //wgv.CreateWorldGrid();
            WorldViewCanvas wv = new WorldViewCanvas(WorldGrid, 600, 600);
            w.OnWorldStep += wv.Draw;
            //wgv.Draw(this, new OnWorldStepArgs(w));
            wv.Draw(this, new OnWorldStepArgs(w));

            /*
            DispatcherTimer loop = new DispatcherTimer();
            loop.Interval = new TimeSpan(0, 0, 0, 1);
            loop.Tick += (sender, e) => { Loops.Loop(w); };
            loop.Start();*/

            Thread t = new Thread(() => { Loops.LoopThread(w); });
            t.IsBackground = true;
            t.Start();
        }
    }


}
