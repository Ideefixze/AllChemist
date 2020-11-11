using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Input;

namespace AllChemist
{
    /// <summary>
    /// Static class for all unified validators for TextBoxes.
    /// </summary>
    public static class TextValidator
    {
        //Only numbers validator
        public static void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
