
using System;

namespace WGADemo.DesignPatterns.Behavioral.Observer
{
    public class MoneyObserver : IObserver<ulong>
    {
        public void OnCompleted()
        {
        }

        public void OnError(Exception error)
        {
        }

        public void OnNext(ulong value)
        {
        }
    }

    public class MoneyObservable : IObservable<ulong>
    {
        public IDisposable Subscribe(IObserver<ulong> observer)
        {
            return default;
        }
    }
}
