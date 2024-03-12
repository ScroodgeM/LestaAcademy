﻿//this empty line for UTF-8 BOM header

namespace LestaAcademyDemo.DesignPatterns.Behavioral.ChainOfResponsibility.Handlers
{
    public class UserInterfaceEventHandler : IScreenInputEventHandler
    {
        public int GetPriority()
        {
            return (int)Priority.UserInterface;
        }

        public bool TryHandle(ScreenInputEvent screenInputEvent)
        {
            // if tap position hits some UI element, do nothing and return true to prevent other handlers processing

            return true;
        }
    }
}
