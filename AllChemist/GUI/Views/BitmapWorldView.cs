using AllChemist.Cells;
using AllChemist.GUI.Controllers;
using AllChemist.Model;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace AllChemist.GUI.Views
{
    /// <summary>
    /// Bitmap that is updated to display current model.
    /// </summary>
    public class BitmapWorldView : GUIContextView, IPaintable
    {
        private Vector2Int originalSize;
        public WriteableBitmap Bitmap;
        public Image Image;

        public BitmapWorldView(MainWindow mainWindow) : base(mainWindow)
        {
            originalSize = new Vector2Int(int.Parse(Properties.Settings.Default.BitmapSizeX), int.Parse(Properties.Settings.Default.BitmapSizeY));

            Image = new Image();
            Image.Width = originalSize.X;
            Image.Height = originalSize.Y;
            RenderOptions.SetBitmapScalingMode(Image, BitmapScalingMode.NearestNeighbor);

            mainWindow.WorldGrid.Children.Clear();
            mainWindow.WorldGrid.Children.Add(Image);

        }

        public void InitModel(World w)
        {
            Bitmap = new WriteableBitmap(w.TableSize.X, w.TableSize.Y, 72, 72, PixelFormats.Bgra32, null);
            uint[] color = new uint[w.CurrentTable.Size.X * w.CurrentTable.Size.Y];

            FullDraw(null, new DrawWorldArgs(w));
            Image.Source = Bitmap;
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

        /// <summary>
        /// Draws a model from beginning to an end.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="args">Args</param>
        public void FullDraw(object sender, DrawWorldArgs args)
        {
            Bitmap.Dispatcher.Invoke(() =>
            {

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
                    }
                }

                Bitmap.WritePixels(new Int32Rect(0, 0, args.CellTable.Size.X, args.CellTable.Size.Y), color, args.CellTable.Size.X * 4, 0);
                s.Stop();
                //Console.WriteLine("FullDraw of the model took: " + s.Elapsed.TotalSeconds + "s");
            });
        }

        /// <summary>
        /// Draws only changes of a model.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="args">Args</param>
        public void DeltaDraw(object sender, DrawWorldArgs args)
        {
            Bitmap.Dispatcher.Invoke(() =>
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

                    Bitmap.WritePixels(new Int32Rect(pos.X, pos.Y, 1, 1), color, 4, 0);
                }
                s.Stop();
                //Console.WriteLine("DeltaDraw of the model took: " + s.Elapsed.TotalSeconds+"s");
            });
        }

        public override void Update(object sender, DrawWorldArgs args)
        {
            DeltaDraw(sender, args);
        }
    }
}
