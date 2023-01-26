
using System.Collections.Generic;

namespace WGADemo.DesignPatterns.Behavioral.Observer
{
    public abstract class SubjectBase<T> : ISubject<T>
    {
        private readonly List<IObserver<T>> observers = new List<IObserver<T>>();

        private T value;

        public T Value
        {
            get
            {
                return value;
            }
            set
            {
                this.value = value;

                foreach (IObserver<T> observer in observers)
                {
                    observer.OnObserverEvent(value);
                }
            }
        }

        public void Subscribe(IObserver<T> observer)
        {
            observers.Add(observer);

            observer.OnObserverEvent(Value);
        }

        public void Unsubscribe(IObserver<T> observer)
        {
            observers.Remove(observer);
        }
    }
}
