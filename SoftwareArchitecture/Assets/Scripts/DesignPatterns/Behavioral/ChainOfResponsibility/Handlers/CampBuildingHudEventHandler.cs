
namespace WGADemo.DesignPatterns.Behavioral.ChainOfResponsibility.Handlers
{
    public class CampBuildingHudEventHandler : IScreenInputEventHandler
    {
        public int GetPriority()
        {
            return 50;
        }

        public bool TryHandle(ScreenInputEvent screenInputEvent)
        {
            // if tap position hits building hud, process this tap and return true

            return false;
        }
    }
}
