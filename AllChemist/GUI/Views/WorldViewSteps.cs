using AllChemist.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using System.Windows.Threading;

namespace AllChemist.GUI.Views
{
    public class WorldViewSteps
    {
        private TextBlock textBlock;
        public WorldViewSteps(TextBlock textBlock)
        {
            this.textBlock = textBlock;
            this.textBlock.Text = "Steps: 0";
        }
        public void Update(object sender, DrawWorldArgs args)
        {
            textBlock.Dispatcher.Invoke(() => { textBlock.Text = "Steps: " + args.Steps.ToString(); });
        }
    }
}
