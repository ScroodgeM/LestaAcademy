
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WGADemo.DesignPatterns.Behavioral.Mediator
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
            // make path to player position and foolow path
        }

        private void Update()
        {
            if (true) // player detected in camera viewport
            {
                Vector3 playerPosition = default; // calculate player position based on camera settings

                mediator.NotifyAboutPlayerPosition(playerPosition);

                // attack player
            }
        }
    }
}
