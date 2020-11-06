using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace AllChemist.GUI.Controllers
{
    interface IPainter
    {
        public void StartPainting(object sender, RoutedEventArgs args);
        public void StopPainting(object sender, RoutedEventArgs args);
    }
}
