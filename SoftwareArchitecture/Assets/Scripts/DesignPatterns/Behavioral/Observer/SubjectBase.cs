//this empty line for UTF-8 BOM header
using System.Collections.Generic;

namespace LestaAcademyDemo.DesignPatterns.Behavioral.Observer
{
    public abstract class SubjectBase<T> : ISubject<T>
    {
        private readonly List<IObserver<T>> observers = new List<IObserver<T>>();

        private T value;

        protected T Value
        {
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

            observer.OnObserverEvent(value);
        }

        public void Unsubscribe(IObserver<T> observer)
        {
            observers.Remove(observer);
        }
    }
}
