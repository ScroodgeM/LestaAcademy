//this empty line for UTF-8 BOM header
using UnityEngine;

namespace LestaAcademyDemo.DesignPatterns.Behavioral.Observer.Observers
{
    public class ShopInformer : MonoBehaviour, IObserver<ulong>
    {
        private void Awake()
        {
            GameController.PlayerMoney.Subscribe(this);
        }

        private void OnDestroy()
        {
            GameController.PlayerMoney.Unsubscribe(this);
        }

        public void OnObserverEvent(ulong value)
        {
            // show notification about [new] goods you can buy now
        }
    }
}
