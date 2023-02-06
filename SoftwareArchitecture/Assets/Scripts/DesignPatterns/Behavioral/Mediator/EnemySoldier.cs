using UnityEngine;

namespace LestaAcademyDemo.DesignPatterns.Behavioral.Mediator
{
    public class EnemySoldier : MonoBehaviour
    {
        private Mediator mediator;

        public void Init(Mediator mediator)
        {
            this.mediator = mediator;

            mediator.OnPlayerDetected += OnPlayerDetected;
        }

        private void OnPlayerDetected(Vector3 playerPosition)
        {
            // make path to player position and follow path
        }

        private void Update()
        {
            if (true) // player detected
            {
                Vector3 playerPosition = default; // calculate player position

                mediator.NotifyAboutPlayerPosition(playerPosition);

                // attack player
            }
        }

        private void OnDestroy()
        {
            mediator.OnPlayerDetected -= OnPlayerDetected;
        }
    }
}
