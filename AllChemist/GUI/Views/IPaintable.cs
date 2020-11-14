using AllChemist.GUI.Controllers;
using System;
using System.Collections.Generic;
using System.Text;

namespace AllChemist.GUI.Views
{
    public interface IPaintable
    {
        public void SetPainter(IPainter painter);
        public void UnsetPainter(IPainter painter);
    }
}
