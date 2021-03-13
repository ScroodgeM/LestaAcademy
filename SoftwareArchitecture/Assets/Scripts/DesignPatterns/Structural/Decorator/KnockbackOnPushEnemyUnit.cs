using UnityEngine;

namespace WGADemo.DesignPatterns.Structural.Decorator
{
    public class KnockbackOnPushEnemyUnit : EnemyUnitDecorator
    {
        public override void Push(int forcePoint)
        {
            base.Push(forcePoint);

            Debug.Log($"push back with {forcePoint} force points");
        }
    }
}
