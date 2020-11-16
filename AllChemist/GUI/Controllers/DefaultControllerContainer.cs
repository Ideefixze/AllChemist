namespace AllChemist.GUI.Controllers
{
    public class DefaultControllerContainer : ControllerContainter
    {

        public DefaultControllerContainer(MainWindow context) : base(context)
        {
            controllers.Add(new ModelSimulationController(context));
            controllers.Add(new SnapshotController(context));
            controllers.Add(new WorldPainterController(context));
            controllers.Add(new WorldSizeController(context));
        }
    }
}
