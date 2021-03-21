
using UnityEngine;

namespace WGADemo.DesignPatterns.Behavioral.Mediator
{
    public class Artillery : MonoBehaviour
    {
        private Mediator mediator;

        public void Init(Mediator mediator)
        {
            this.mediator = mediator;

            mediator.OnPlayerDetected += OnPlayerDetected;
        }

        private void OnPlayerDetected(Vector3 playerPosition)
        {
            if (true) // artillery ready to fire (e.g. not on cooldown after previous action)
            {
                if (true) // check whether player position is in effect area of this artillery
                {
                    // aim to known player position and fire
                }
            }
        }

        private void OnDestroy()
        {
            mediator.OnPlayerDetected -= OnPlayerDetected;
        }
    }
}
