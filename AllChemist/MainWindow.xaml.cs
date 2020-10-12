using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.Title = "AllChemist "+App.Version+"v";
            CreateWorldGrid();
        }

        public void CreateWorldGrid(int sizeX = 600, int sizeY = 600, int columns = 20, int rows = 20)
        {
            Grid worldGrid = new Grid();
            worldGrid.Width = sizeX;
            worldGrid.Height = sizeY;
            worldGrid.HorizontalAlignment = HorizontalAlignment.Left;
            worldGrid.VerticalAlignment = VerticalAlignment.Center;
            worldGrid.ShowGridLines = true;


            for (int i = 0; i < columns; i++)
            {
                ColumnDefinition cd = new ColumnDefinition();
                cd.MaxWidth = sizeX / columns;
                worldGrid.ColumnDefinitions.Add(cd);
            }

            for (int i = 0; i < rows; i++)
            {
                RowDefinition rd = new RowDefinition();
                rd.MaxHeight = sizeY / rows;
                worldGrid.RowDefinitions.Add(rd);
            }

            for (int i = 0; i < columns; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    Grid g = new Grid();
                    g.HorizontalAlignment = HorizontalAlignment.Stretch;
                    g.VerticalAlignment = VerticalAlignment.Center;
                    

                    Emoji.Wpf.TextBlock textBlock = new Emoji.Wpf.TextBlock();
                    textBlock.Text = "◼";
                    textBlock.FontSize = 16;
                    textBlock.TextAlignment = TextAlignment.Center;
                    textBlock.MaxHeight = sizeY / rows;
                    textBlock.MaxWidth = sizeX / columns;

                    g.Children.Add(textBlock);

                    Grid.SetRow(g, j);
                    Grid.SetColumn(g, i);
                    worldGrid.Children.Add(g);
                }
            }

            MainGrid.Children.Add(worldGrid);
        }
    }
}
