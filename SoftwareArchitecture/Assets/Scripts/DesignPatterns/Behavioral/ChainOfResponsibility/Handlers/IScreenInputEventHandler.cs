namespace LestaAcademyDemo.DesignPatterns.Behavioral.ChainOfResponsibility.Handlers
{
    public interface IScreenInputEventHandler
    {
        int GetPriority();
        bool TryHandle(ScreenInputEvent screenInputEvent);
    }
}
