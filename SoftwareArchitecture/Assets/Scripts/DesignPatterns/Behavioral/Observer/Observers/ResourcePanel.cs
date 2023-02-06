//this empty line for UTF-8 BOM header
using UnityEngine;

namespace LestaAcademyDemo.DesignPatterns.Behavioral.Observer.Observers
{
    public class ResourcePanel : MonoBehaviour, IObserver<ulong>
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
            // update money label in resource panel
        }
    }
}
