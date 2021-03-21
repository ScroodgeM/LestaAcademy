
using System;
using UnityEngine;

namespace WGADemo.DesignPatterns.Behavioral.Mediator
{
    public class Mediator
    {
        public event Action<Vector3> OnPlayerDetected = position => { };

        public void NotifyAboutPlayerPosition(Vector3 playerPosition)
        {
            OnPlayerDetected(playerPosition);
        }
    }
}
