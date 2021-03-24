
using WGADemo.DesignPatterns.Behavioral.ChainOfResponsibility.Handlers;

namespace WGADemo.DesignPatterns.Behavioral.ChainOfResponsibility
{
    public interface IInputSystem
    {
        void RegisterHandler(IScreenInputEventHandler handler);
    }
}
