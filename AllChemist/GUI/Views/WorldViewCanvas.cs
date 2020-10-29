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

            main.Children.Clear();
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
}