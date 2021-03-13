
using UnityEngine;

namespace WGADemo.DesignPatterns.Structural.Adapter.Units
{
    public interface INPC
    {
        void MoveToPoint(Vector3 position);
        void Stop();
    }
}
