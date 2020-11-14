using System;
using System.Collections.Generic;
using System.Text;

namespace AllChemist.GUI.Views
{
    public class DefaultViewContainer : ViewContainer
    {
        public DefaultViewContainer(MainWindow context) : base(context)
        {
            views.Add(new BitmapWorldView(context));
            views.Add(new StepsView(context));

        }
    }
}
