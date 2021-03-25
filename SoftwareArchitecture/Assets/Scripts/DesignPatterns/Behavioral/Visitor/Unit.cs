﻿
using UnityEngine;

namespace WGADemo.DesignPatterns.Behavioral.Visitor
{
    public abstract class Unit : MonoBehaviour
    {
        public void SetSpeed(float speed)
        {
            // apply speed here
        }

        public void ApplyDamage(int damagePoints)
        {
            // apply damage here
        }

        public abstract void Accept(IVisitor visitor);
    }
}
