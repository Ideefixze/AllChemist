using AllChemist.Cells;
using AllChemist.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using Xceed.Wpf.Toolkit;

namespace AllChemist.GUI.Controllers
{
    /// <summary>
    /// Simple painter and controller for putting down cells on a canvas. Handles color picker so that it always shows colors from CellTypeBank
    /// </summary>
    public class WorldPainterController : GUIContextController, IPainter
    {
        private World world;
        private ColorPicker colorPicker;
        private Thread paintingThread;
        private bool isPainting;
        public WorldPainterController(MainWindow context) : base(context)
        {
            colorPicker = context.CellColorPicker;
        }

        public override void SetUpModel(World world)
        {
            this.world = world;

            colorPicker.ShowRecentColors = false;
            colorPicker.ShowStandardColors = false;
            colorPicker.ShowTabHeaders = false;
            colorPicker.AvailableColors.Clear();
            foreach (KeyValuePair<int, CellType> kv in world.CellTypeBank.CellTypes)
            {
                colorPicker.AvailableColors.Add(new ColorItem(kv.Value.Color, kv.Value.Name));
            }
            colorPicker.SelectedColor = colorPicker.AvailableColors[0].Color;
        }


        public CellType GetCurrentCellType(CellTypeBank cellTypeBank)
        {
            //WARNING: No duplicate colors in CellTypeBank please
            return cellTypeBank.CellTypes.FirstOrDefault(t => t.Value.Color == colorPicker.SelectedColor).Value;
        }

        public void StartPainting(object sender, RoutedEventArgs args)
        {
            isPainting = true;
            paintingThread = new Thread(() => { PaintWorld((FrameworkElement)sender); });
            paintingThread.Start();
        }

        public void StopPainting(object sender, RoutedEventArgs args)
        {
            isPainting = false;
        }

        public void PaintWorld(FrameworkElement on)
        {
            while (isPainting)
            {
                lock (world)
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        Point mousePoint = Mouse.GetPosition(on);
                        double xx = mousePoint.X / on.ActualWidth;
                        double yy = mousePoint.Y / on.ActualHeight;
                        Vector2Int worldPos = new Vector2Int((int)(xx * world.TableSize.X), (int)(yy * world.TableSize.Y));
                        this.PaintWorld(worldPos);
                        world.ApplyChanges();
                    });

                }
                Thread.Sleep(50);
            }

        }

        private void PaintWorld(Vector2Int pos)
        {
            world.Paint(pos, GetCurrentCellType(world.CellTypeBank), EPaintType.PAINT_CURRENT);
        }


    }
}