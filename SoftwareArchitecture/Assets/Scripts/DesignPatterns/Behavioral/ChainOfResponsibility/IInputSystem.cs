using LestaAcademyDemo.DesignPatterns.Behavioral.ChainOfResponsibility.Handlers;

namespace LestaAcademyDemo.DesignPatterns.Behavioral.ChainOfResponsibility
{
    public interface IInputSystem
    {
        void RegisterHandler(IScreenInputEventHandler handler);
    }
}
