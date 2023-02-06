//this empty line for UTF-8 BOM header
using UnityEngine;

namespace LestaAcademyDemo.DesignPatterns.Structural.Adapter.Units
{
    public interface INPC
    {
        void MoveToPoint(Vector3 position);
        void Stop();
    }
}
