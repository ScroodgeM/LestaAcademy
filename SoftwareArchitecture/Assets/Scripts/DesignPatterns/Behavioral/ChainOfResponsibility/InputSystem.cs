//this empty line for UTF-8 BOM header
using System.Collections.Generic;
using LestaAcademyDemo.DesignPatterns.Behavioral.ChainOfResponsibility.Handlers;

namespace LestaAcademyDemo.DesignPatterns.Behavioral.ChainOfResponsibility
{
    public class InputSystem : IInputSystem
    {
        private readonly List<IScreenInputEventHandler> allHandlers = new List<IScreenInputEventHandler>();

        public void RegisterHandler(IScreenInputEventHandler handler)
        {
            allHandlers.Add(handler);

            allHandlers.Sort((a, b) => a.GetPriority().CompareTo(b.GetPriority()));
        }

        public void HandleEvent(ScreenInputEvent screenInputEvent)
        {
            foreach (IScreenInputEventHandler handler in allHandlers)
            {
                if (handler.TryHandle(screenInputEvent) == true)
                {
                    break;
                }
            }
        }
    }
}
