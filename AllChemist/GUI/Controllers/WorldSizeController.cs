using AllChemist.Model;
using System;
using System.Windows.Controls;

namespace AllChemist.GUI.Controllers
{
    /// <summary>
    /// Controller for setting and getting the world size for a newly created model.
    /// </summary>
    public class WorldSizeController : GUIContextController
    {
        private TextBox XTextBox;
        private TextBox YTextBox;

        public WorldSizeController(MainWindow context) : base(context)
        {
            XTextBox = context.SizeXTextBox;
            YTextBox = context.SizeYTextBox;

            XTextBox.PreviewTextInput += TextValidator.NumberValidationTextBox;
            YTextBox.PreviewTextInput += TextValidator.NumberValidationTextBox;
        }

        public override void SetUpModel(World world)
        {

        }

        public Vector2Int GetWorldSize()
        {
            return new Vector2Int(Math.Max(int.Parse(XTextBox.Text), 1), Math.Max(int.Parse(YTextBox.Text), 1));
        }

    }
}