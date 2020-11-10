using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace AllChemist.GUI
{
    public static class DialogInput
    {
        /// <summary>
        /// Shows dialog that stops current program until we enter some input.
        /// </summary>
        /// <returns></returns>
        public static string StringForm()
        {
            string returnValue="";

            Window form = new Window();

            form.Width = 196;
            form.Height = 128;

            Grid grid = new Grid();
            form.Content = grid;
            grid.ColumnDefinitions.Add(new ColumnDefinition());
            grid.RowDefinitions.Add(new RowDefinition());
            grid.RowDefinitions.Add(new RowDefinition());

            TextBox inputTextBox = new TextBox();
            inputTextBox.Width = 96;
            inputTextBox.Height = 32;

            Button button = new Button();
            button.Width = 64;
            button.Height = 32;
            button.Click += (s, e) => { returnValue = inputTextBox.Text; form.Close(); };

            Grid.SetRow(inputTextBox, 0);
            grid.Children.Add(inputTextBox);
            Grid.SetRow(button, 1);
            grid.Children.Add(button);
            form.ShowDialog();

            return returnValue;
        }
    }
}
