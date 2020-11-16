using AllChemist.Model;

namespace AllChemist.GUI.Controllers
{
    /// <summary>
    /// Abstract class for all Controllers that are somewhat associated with GUI
    /// </summary>
    public abstract class GUIContextController : IController
    {
        public GUIContextController(MainWindow context)
        {

        }

        public abstract void SetUpModel(World world);

        public virtual void CleanUpModel(World world)
        {
        }
    }
}
