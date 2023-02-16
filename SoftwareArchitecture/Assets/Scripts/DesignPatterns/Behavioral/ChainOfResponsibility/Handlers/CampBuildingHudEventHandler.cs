//this empty line for UTF-8 BOM header

namespace LestaAcademyDemo.DesignPatterns.Behavioral.ChainOfResponsibility.Handlers
{
    public class CampBuildingHudEventHandler : IScreenInputEventHandler
    {
        public int GetPriority()
        {
            return (int)Priority.CampBuildingHud;
        }

        public bool TryHandle(ScreenInputEvent screenInputEvent)
        {
            // if tap position hits building hud, process this tap and return true

            return false;
        }
    }
}
