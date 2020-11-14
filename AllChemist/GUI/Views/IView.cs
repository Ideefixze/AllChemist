using AllChemist.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace AllChemist.GUI.Views
{
    public interface IView
    {
        public void Update(object sender, DrawWorldArgs args);
    }
}
