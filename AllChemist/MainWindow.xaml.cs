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
                        solidColorBrush.Color = args.World.CurrentTable.Cells[i, j].CellType.color;
                        rect.Fill = solidColorBrush;
                        canvas.Children.Add(rect);

                        Rectangle rectGrid = new Rectangle();
                        rect.Width = canvas.Width/ args.World.CurrentTable.Size.x;
                        rect.Height = canvas.Height / args.World.CurrentTable.Size.y;

                        rect.Stroke = Brushes.Black;
                    
                        canvas.Children.Add(rectGrid);


                        Canvas.SetLeft(rect, j*canvas.Width / args.World.CurrentTable.Size.x);
                        Canvas.SetTop(rect, i*canvas.Height / args.World.CurrentTable.Size.y);
                    }
                
                }
            });


        }
    }
    
    public class Loops
    {
        public static void Loop(World w)
        {
            w?.Step();
        }

        public static void LoopThread(World w, int milisecondsDelay)
        {
            while(true)
            {
                w?.Step();
                Thread.Sleep(milisecondsDelay);
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

            CellType typeA = new CellType(255,0,0);
            CellType typeB = new CellType(0,255,0);


            typeA.CellBehaviour.Rules.Add(new SwapToRule(typeB));
            typeB.CellBehaviour.Rules.Add(new SwapToRule(typeA));

            World w = new World(new Vector2Int(30, 30), typeA);
            WorldViewCanvas wv = new WorldViewCanvas(WorldGrid, 600, 600);
            w.OnWorldStep += wv.Draw;
            wv.Draw(this, new OnWorldStepArgs(w));


            Thread t = new Thread(() => { Loops.LoopThread(w,500); });
            t.IsBackground = true;
            t.Start();
        }
    }


}
