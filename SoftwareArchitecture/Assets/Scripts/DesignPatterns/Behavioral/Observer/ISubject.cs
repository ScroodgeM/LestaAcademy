namespace LestaAcademyDemo.DesignPatterns.Behavioral.Observer
{
    public interface ISubject<T>
    {
        T Value { get; }
        void Subscribe(IObserver<T> observer);
        void Unsubscribe(IObserver<T> observer);
    }
}
