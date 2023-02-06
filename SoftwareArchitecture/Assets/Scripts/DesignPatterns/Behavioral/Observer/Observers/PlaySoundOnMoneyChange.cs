using UnityEngine;

namespace LestaAcademyDemo.DesignPatterns.Behavioral.Observer.Observers
{
    public class PlaySoundOnMoneyChange : MonoBehaviour, IObserver<ulong>
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
            // play sound
        }
    }
}
