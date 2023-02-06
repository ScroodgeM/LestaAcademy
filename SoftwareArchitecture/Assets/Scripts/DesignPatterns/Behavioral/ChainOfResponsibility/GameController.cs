//this empty line for UTF-8 BOM header
using System.Collections.Generic;
using LestaAcademyDemo.DesignPatterns.Behavioral.ChainOfResponsibility.Handlers;

namespace LestaAcademyDemo.DesignPatterns.Behavioral.ChainOfResponsibility
{
    public class GameController
    {
        private readonly IInputSystem inputSystem;

        public GameController(IInputSystem inputSystem)
        {
            this.inputSystem = inputSystem;
        }

        public void SubscribeThem()
        {
            foreach (IScreenInputEventHandler handler in GetAllHandlers())
            {
                inputSystem.RegisterHandler(handler);
            }
        }

        private IEnumerable<IScreenInputEventHandler> GetAllHandlers()
        {
            yield return new CampBuildingEventHandler();

            yield return new UserInterfaceEventHandler();

            yield return new CampBuildingHudEventHandler();

            yield return new CameraMoverEventHandler();
        }
    }
}
