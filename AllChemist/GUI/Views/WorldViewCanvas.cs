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
using AllChemist.Model;

namespace AllChemist.GUI.Views
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
            bool drawGrid = (w.TableSize.X * w.TableSize.Y) < 1000;
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
                    
                    if(drawGrid)
                        rect.Stroke = Brushes.Black;
                    
                    rect.ToolTip = $"{w.CurrentTable.Cells[i, j].CellType.name} ({w.CurrentTable.Cells[i, j].CellType.id})\nAt position ({j},{w.CurrentTable.Size.X - i - 1})";
                    Canvas.Children.Add(rect);

                    Canvas.SetLeft(rect, i * Canvas.Width / w.CurrentTable.Size.X);
                    Canvas.SetTop(rect, j * Canvas.Height / w.CurrentTable.Size.Y);

                    rectangles[i, j] = rect;
                }
            }
            
        }

        public void Draw(object sender, DrawWorldArgs args)
        {
            Canvas.Dispatcher.Invoke(() => {
                foreach(Vector2Int pos in args.Delta)
                {
                    ((SolidColorBrush)rectangles[pos.X, pos.Y].Fill).Color = args.CellTable.Cells[pos.X, pos.Y].CellType.color;

                    rectangles[pos.X, pos.Y].ToolTip = $"{args.CellTable.Cells[pos.X, pos.Y].CellType.name} ({args.CellTable.Cells[pos.X, pos.Y].CellType.id})\nAt position ({pos.Y},{args.CellTable.Size.X - pos.X - 1})";
                }
                
            });
        }
    }
}