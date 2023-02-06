//this empty line for UTF-8 BOM header
using System;
using UnityEngine;

namespace LestaAcademyDemo.DesignPatterns.Behavioral.Mediator
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
