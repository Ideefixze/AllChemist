using System;
using System.Collections.Generic;
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

namespace AllChemist
{
    public class WorldGridView
    {

        private Vector2Int size;
        private Grid mainGrid;
        private World world;

        public WorldGridView(World world, Grid mainGrid)
        {
            this.size = world.TableSize;
            this.world = world;
            this.mainGrid = mainGrid;
        }
        public void CreateWorldGrid(int sizeX = 600, int sizeY = 600)
        {
            Grid worldGrid = new Grid();
            worldGrid.Width = sizeX;
            worldGrid.Height = sizeY;
            worldGrid.HorizontalAlignment = HorizontalAlignment.Left;
            worldGrid.VerticalAlignment = VerticalAlignment.Center;
            worldGrid.ShowGridLines = true;


            for (int i = 0; i < size.x; i++)
            {
                ColumnDefinition cd = new ColumnDefinition();
                cd.MaxWidth = sizeX / size.x;
                worldGrid.ColumnDefinitions.Add(cd);
            }

            for (int i = 0; i < size.y; i++)
            {
                RowDefinition rd = new RowDefinition();
                rd.MaxHeight = sizeY / size.y;
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
                    textBlock.Text = world.CurrentTable.Cells[i,j].CellArt;
                    textBlock.FontSize = 16;
                    textBlock.TextAlignment = TextAlignment.Center;
                    textBlock.MaxHeight = sizeY / size.y;
                    textBlock.MaxWidth = sizeX / size.x;

                    g.Children.Add(textBlock);

                    Grid.SetRow(g, j);
                    Grid.SetColumn(g, i);
                    worldGrid.Children.Add(g);
                }
            }

            mainGrid.Children.Add(worldGrid);
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
            World w = new World(new Vector2Int(20, 20), new ExistingCell(new Vector2Int(0, 0), "x"));
            WorldGridView wgv = new WorldGridView(w,MainGrid);
            wgv.CreateWorldGrid();
        }
    }


}
