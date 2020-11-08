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
using AllChemist.Cells;

namespace AllChemist.GUI.Controllers
{
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
                        Vector2Int worldPos = new Vector2Int((int)mousePoint.X / ((int)on.Width / world.TableSize.X), (int)mousePoint.Y / ((int)on.Height / world.TableSize.Y));
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