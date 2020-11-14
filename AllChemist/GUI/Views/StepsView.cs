using AllChemist.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using System.Windows.Threading;

namespace AllChemist.GUI.Views
{
    public class StepsView : GUIContextView
    {
        private TextBlock textBlock;
        public StepsView(MainWindow mainWindow) : base(mainWindow)
        {
            this.textBlock = mainWindow.StepsTextBlock;
            this.textBlock.Text = "Steps: 0";
        }
        public override void Update(object sender, DrawWorldArgs args)
        {
            textBlock.Dispatcher.Invoke(() => { textBlock.Text = "Steps: " + args.Steps.ToString(); });
        }
    }
}
