using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace AllChemist.GUI.Controllers
{
    /// <summary>
    /// Painter interface that has two should implement two functionalities: on mouse being pressed down in the canvsas and on mouse being pressed up.
    /// </summary>
    public interface IPainter
    {
        public void StartPainting(object sender, RoutedEventArgs args);
        public void StopPainting(object sender, RoutedEventArgs args);
    }
}
