//this empty line for UTF-8 BOM header

using UnityEngine;

namespace LestaAcademyDemo.DesignPatterns.Behavioral.Observer.Observers
{
    public class PlaySoundOnMoneyChange : MonoBehaviour, IObserver<Money>
    {
        private ISubject<Money> playerMoney;

        public void Init(GameController gameController)
        {
            playerMoney = gameController.PlayerMoney;
        }

        private void Start()
        {
            playerMoney.Subscribe(this);
        }

        private void OnDestroy()
        {
            playerMoney.Unsubscribe(this);
        }

        public void OnObserverEvent(Money value)
        {
            // play sound
        }
    }
}
