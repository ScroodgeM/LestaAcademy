
using UnityEngine;

namespace LestaAcademyDemo.DesignPatterns.Structural.Decorator
{
    public class DamagePlayerOnHitUnit : UnitDecorator
    {
        public override void Damage(int damagePoints)
        {
            base.Damage(damagePoints);

            Debug.Log($"Damage player back with {damagePoints} points");
        }
    }
}
