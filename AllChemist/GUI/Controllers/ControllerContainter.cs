using AllChemist.Model;
using System.Collections.Generic;
using System.Linq;

namespace AllChemist.GUI.Controllers
{

    /// <summary>
    /// Contains all Controllers in our App and prepares them for usage.
    /// </summary>
    public class ControllerContainter : GUIContextController
    {
        protected List<GUIContextController> controllers;
        public ControllerContainter(MainWindow context) : base(context)
        {
            controllers = new List<GUIContextController>();

        }

        public override void SetUpModel(World world)
        {
            foreach (GUIContextController c in controllers)
            {
                c.SetUpModel(world);
            }
        }

        public override void CleanUpModel(World world)
        {
            foreach (GUIContextController c in controllers)
            {
                c.CleanUpModel(world);
            }
        }

        public T GetController<T>()
        {
            return controllers.OfType<T>().First();
        }
    }
}
