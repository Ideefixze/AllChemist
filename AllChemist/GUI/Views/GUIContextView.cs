using AllChemist.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace AllChemist.GUI.Views
{
    public abstract class GUIContextView : IView
    {
        public GUIContextView(MainWindow mainWindow)
        {

        }

        public virtual void Update(object sender, DrawWorldArgs args)
        {
            throw new NotImplementedException();
        }
    }
}
