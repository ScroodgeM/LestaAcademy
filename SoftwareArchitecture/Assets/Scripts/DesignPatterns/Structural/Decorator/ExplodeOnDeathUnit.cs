using UnityEngine;

namespace LestaAcademyDemo.DesignPatterns.Structural.Decorator
{
    public class ExplodeOnDeathUnit : UnitDecorator
    {
        private readonly float explodeRadius;

        public ExplodeOnDeathUnit(float explodeRadius)
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
