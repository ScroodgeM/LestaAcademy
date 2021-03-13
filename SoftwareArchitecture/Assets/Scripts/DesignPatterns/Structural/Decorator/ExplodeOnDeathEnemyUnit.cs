using UnityEngine;

namespace WGADemo.DesignPatterns.Structural.Decorator
{
    public class ExplodeOnDeathEnemyUnit : EnemyUnitDecorator
    {
        private readonly float explodeRadius;

        public ExplodeOnDeathEnemyUnit(float explodeRadius)
        {
            this.explodeRadius = explodeRadius;
        }

        public override void Kill()
        {
            base.Kill();

            Debug.Log($"explode and damage everything in radius {explodeRadius}");
        }
    }
}
