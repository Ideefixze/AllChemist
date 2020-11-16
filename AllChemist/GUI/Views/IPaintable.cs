using AllChemist.GUI.Controllers;

namespace AllChemist.GUI.Views
{
    public interface IPaintable
    {
        public void SetPainter(IPainter painter);
        public void UnsetPainter(IPainter painter);
    }
}
