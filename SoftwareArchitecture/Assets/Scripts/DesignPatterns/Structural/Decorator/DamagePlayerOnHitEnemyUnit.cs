using UnityEngine;

namespace WGADemo.DesignPatterns.Structural.Decorator
{
    public class DamagePlayerOnHitEnemyUnit : EnemyUnitDecorator
    {
        public override void Damage(int damagePoints)
        {
            base.Damage(damagePoints);

            Debug.Log($"Damage player back with {damagePoints} points");
        }
    }
}
