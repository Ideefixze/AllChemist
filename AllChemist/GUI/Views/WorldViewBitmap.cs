using AllChemist.Cells;
using AllChemist.GUI.Controllers;
using AllChemist.Model;
using Newtonsoft.Json.Bson;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace AllChemist.GUI.Views
{
    class WorldViewBitmap
    {
        private Vector2Int originalSize;
        private WriteableBitmap bitmap;
        public Image Image;

        public WorldViewBitmap(Grid main, Vector2Int size)
        {
            originalSize = size;
            //image = new System.Windows.Controls.Image();
            Image = new Image();
            Image.Width = size.X;
            Image.Height = size.Y;
            RenderOptions.SetBitmapScalingMode(Image, BitmapScalingMode.NearestNeighbor);
            //Image.ActualWidth = size.X;
            //Image.A

            main.Children.Clear();
            main.Children.Add(Image);

        }

        public void InitModel(World w)
        {
            bitmap = new WriteableBitmap(w.TableSize.X, w.TableSize.Y, 72, 72, PixelFormats.Bgra32, null);
            uint[] color = new uint[w.CurrentTable.Size.X * w.CurrentTable.Size.Y];

            FullDraw(null, new DrawWorldArgs(w));
            Image.Source = bitmap;
        }

        public void SetPainter(IPainter painter)
        {
            Image.MouseLeftButtonDown += painter.StartPainting;
            Image.MouseLeftButtonUp += painter.StopPainting;
            Image.MouseLeave += painter.StopPainting;
        }

        public void UnsetPainter(IPainter painter)
        {
            Image.MouseLeftButtonDown -= painter.StartPainting;
            Image.MouseLeftButtonUp -= painter.StopPainting;
            Image.MouseLeave -= painter.StopPainting;
        }

        public void FullDraw(object sender, DrawWorldArgs args)
        {
            bitmap.Dispatcher.Invoke(() => {
                /*foreach (Vector2Int pos in args.Delta)
                {
                    ((SolidColorBrush)rectangles[pos.X, pos.Y].Fill).Color = args.CellTable.Cells[pos.X, pos.Y].CellType.color;

                    rectangles[pos.X, pos.Y].ToolTip = $"{args.CellTable.Cells[pos.X, pos.Y].CellType.name} ({args.CellTable.Cells[pos.X, pos.Y].CellType.id})\nAt position ({pos.Y},{args.CellTable.Size.X - pos.X - 1})";
                }*/
                Stopwatch s = new Stopwatch();
                s.Start();
                byte[] color = new byte[args.CellTable.Size.X * args.CellTable.Size.Y * 4];

                for (int i = 0; i < args.CellTable.Size.X; i++)
                {
                    for (int j = 0; j < args.CellTable.Size.Y; j++)
                    {
                        int p = args.CellTable.Size.X * j + i;
                        CellType c = args.CellTable.Cells[i, j].CellType;
                        color[4 * p] = c.Color.B;
                        color[4 * p + 1] = c.Color.G;
                        color[4 * p + 2] = c.Color.R;
                        color[4 * p + 3] = c.Color.A;
                        //Console.WriteLine($"{color[p]} and {uint.MaxValue}");
                    }
                }

                bitmap.WritePixels(new Int32Rect(0, 0, args.CellTable.Size.X, args.CellTable.Size.Y), color, args.CellTable.Size.X * 4, 0);
                s.Stop();
                Console.WriteLine("FullDraw of the model took: " + s.Elapsed.TotalSeconds + "s");
            });
        }

        public void DeltaDraw(object sender, DrawWorldArgs args)
        {
            bitmap.Dispatcher.Invoke(() =>
            {
                Stopwatch s = new Stopwatch();
                s.Start();
                foreach (Vector2Int pos in args.Delta)
                {
                    byte[] color = new byte[4];

                    CellType c = args.CellTable.Cells[pos.X, pos.Y].CellType;
                    color[0] = c.Color.B;
                    color[1] = c.Color.G;
                    color[2] = c.Color.R;
                    color[3] = c.Color.A;

                    bitmap.WritePixels(new Int32Rect(pos.X, pos.Y, 1, 1), color, 4, 0);
                }
                s.Stop();
                Console.WriteLine("DeltaDraw of the model took: " + s.Elapsed.TotalSeconds+"s");
            });
        }
    }
}
