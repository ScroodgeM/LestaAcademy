namespace LestaAcademyDemo.DesignPatterns.Behavioral.ChainOfResponsibility.Handlers
{
    class CampBuildingEventHandler : IScreenInputEventHandler
    {
        public int GetPriority()
        {
            return 120;
        }

        public bool TryHandle(ScreenInputEvent screenInputEvent)
        {
            if (screenInputEvent.longTap == true)
            {
                // compare building interactable zone with screen tap position

                // select building if hit success

                return true;
            }

            return false;
        }
    }
}
