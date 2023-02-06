//this empty line for UTF-8 BOM header
using UnityEngine;

namespace LestaAcademyDemo.DesignPatterns.Structural.Composite
{
    public interface IUnit
    {
        bool ArrivedToDestination { get; }
        void Fire(Vector3 aimPoint);
        float GetHealth();
    }
}
