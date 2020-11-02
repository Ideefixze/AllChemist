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
    public class WorldPainterController
    {
        private World world;
        private ColorPicker colorPicker;
        private Thread paintingThread;
        private bool isPainting;
        public WorldPainterController(ColorPicker cp, World w)
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
        }

        public void PaintWorld(Canvas canvas)
        {
            while (isPainting)
            {
                lock (world)
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        Point mousePoint = Mouse.GetPosition(canvas);
                        Vector2Int worldPos = new Vector2Int((int)mousePoint.X / ((int)canvas.Width / world.TableSize.X), (int)mousePoint.Y / ((int)canvas.Height / world.TableSize.Y));
                        this.PaintWorld(worldPos);
                        world.ApplyChanges();
                    });
                    Thread.Sleep(20);
                }
            }
            
        }

        private void PaintWorld(Vector2Int pos)
        {
             world.Paint(pos, GetCurrentCellType(world.CellTypeBank));
        }
    }
}