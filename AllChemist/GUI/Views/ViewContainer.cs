using AllChemist.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllChemist.GUI.Views
{
    public class ViewContainer : GUIContextView
    {
        protected List<GUIContextView> views;
        public ViewContainer(MainWindow context) : base(context)
        {
            views = new List<GUIContextView>();
        }

        public override void Update(object sender, DrawWorldArgs args)
        {
            foreach(GUIContextView v in views)
            {
                v.Update(sender, args);
            }
        }

        public T GetView<T>()
        {
            return views.OfType<T>().First();
        }
    }
}
