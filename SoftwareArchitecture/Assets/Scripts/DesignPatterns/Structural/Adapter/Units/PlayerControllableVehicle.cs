﻿//this empty line for UTF-8 BOM header

using UnityEngine;

namespace LestaAcademyDemo.DesignPatterns.Structural.Adapter.Units
{
    public class PlayerControllableVehicle : Vehicle, IPlayerControllable
    {
        [SerializeField] private float moveTargetOffsetForManualControl;

        public void FireCommand()
        {
            base.Beep();
        }

        public void JumpCommand()
        {
            // jump away from car and release control over car
        }

        public void MoveCommand(Vector2 direction)
        {
            if (direction != Vector2.zero)
            {
                Vector3 direction3d = new Vector3(direction.x, 0, direction.y);

                Vector3 targetPoint = base.CurrentPosition + direction3d * moveTargetOffsetForManualControl;

                base.MoveToPoint(targetPoint);
            }
            else
            {
                base.Stop();
            }
        }
    }
}
