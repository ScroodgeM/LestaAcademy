
namespace WGADemo.DesignPatterns.Behavioral.Observer
{
    public interface IObserver<T>
    {
        void OnObserverEvent(T value);
    }
}
