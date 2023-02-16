//this empty line for UTF-8 BOM header

using UnityEngine;

namespace LestaAcademyDemo.DesignPatterns.Behavioral.Mediator
{
    public class SecurityCamera : MonoBehaviour
    {
        private Mediator mediator;

        public void Init(Mediator mediator)
        {
            this.mediator = mediator;
        }

        private void Update()
        {
            if (true) // player detected in camera viewport
            {
                Vector3 playerPosition = default; // calculate player position based on camera settings

                mediator.NotifyAboutPlayerPosition(playerPosition);
            }
        }
    }
}
