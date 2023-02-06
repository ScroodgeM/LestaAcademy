using UnityEngine;

namespace LestaAcademyDemo.DesignPatterns.Structural.Adapter.Units
{
    public interface INPC
    {
        void MoveToPoint(Vector3 position);
        void Stop();
    }
}
