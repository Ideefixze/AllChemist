using System.Windows;
using System.Windows.Controls;

namespace AllChemist.GUI
{
    public static class DialogInput
    {
        /// <summary>
        /// Shows dialog that stops current program until we enter some input.
        /// </summary>
        /// <returns>String from user's input</returns>
        public static string StringForm(string windowTitle = "", string labelText = "", string defaultTextBoxValue = "")
        {
            string returnValue = "";

            Window form = new Window();
            form.Title = windowTitle;

            form.Width = 196;
            form.Height = 128;

            Grid grid = new Grid();
            form.Content = grid;
            grid.ColumnDefinitions.Add(new ColumnDefinition());
            grid.RowDefinitions.Add(new RowDefinition());
            grid.RowDefinitions.Add(new RowDefinition());
            grid.RowDefinitions.Add(new RowDefinition());
            grid.HorizontalAlignment = HorizontalAlignment.Center;

            Label label = new Label();
            label.Content = labelText;

            TextBox inputTextBox = new TextBox();
            inputTextBox.Width = 96;
            inputTextBox.Height = 32;
            inputTextBox.Text = defaultTextBoxValue;

            Button button = new Button();
            button.Width = 64;
            button.Height = 32;
            button.Click += (s, e) => { returnValue = inputTextBox.Text; form.Close(); };
            button.Content = "OK";

            Grid.SetRow(label, 0);
            grid.Children.Add(label);
            Grid.SetRow(inputTextBox, 1);
            grid.Children.Add(inputTextBox);
            Grid.SetRow(button, 2);
            grid.Children.Add(button);
            form.ShowDialog();

            return returnValue;
        }
    }
}
