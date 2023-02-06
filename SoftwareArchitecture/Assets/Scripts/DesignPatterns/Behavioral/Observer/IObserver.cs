//this empty line for UTF-8 BOM header

namespace LestaAcademyDemo.DesignPatterns.Behavioral.Observer
{
    public interface IObserver<T>
    {
        void OnObserverEvent(T value);
    }
}
