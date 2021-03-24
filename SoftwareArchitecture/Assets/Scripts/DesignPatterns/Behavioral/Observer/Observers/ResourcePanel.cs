
using UnityEngine;

namespace WGADemo.DesignPatterns.Behavioral.Observer.Observers
{
    public class ResourcePanel : MonoBehaviour
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
            // update money label in resource panel
        }
    }
}
