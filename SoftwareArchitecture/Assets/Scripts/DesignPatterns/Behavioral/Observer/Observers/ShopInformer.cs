
using UnityEngine;

namespace WGADemo.DesignPatterns.Behavioral.Observer.Observers
{
    public class ShopInformer : MonoBehaviour
    {
        private void Awake()
        {
            GameController.PlayerState.OnMoneyUpdated += PlayerState_OnMoneyUpdated;
        }

        private void OnDestroy()
        {
            GameController.PlayerState.OnMoneyUpdated -= PlayerState_OnMoneyUpdated;
        }

        private void PlayerState_OnMoneyUpdated()
        {
            // show notification about [new] goods you can buy now
        }
    }
}
