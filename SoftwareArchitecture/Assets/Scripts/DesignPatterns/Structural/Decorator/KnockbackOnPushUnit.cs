
using UnityEngine;

namespace LestaAcademyDemo.DesignPatterns.Structural.Decorator
{
    public class KnockbackOnPushUnit : UnitDecorator
    {
        public override void Push(int forcePoint)
        {
            base.Push(forcePoint);

            Debug.Log($"push back with {forcePoint} force points");
        }
    }
}
