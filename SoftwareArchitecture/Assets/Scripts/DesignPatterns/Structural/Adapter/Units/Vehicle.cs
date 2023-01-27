
using UnityEngine;

namespace LestaAcademyDemo.DesignPatterns.Structural.Adapter.Units
{
    public class Vehicle : MonoBehaviour, INPC
    {
        protected void Beep()
        {
            // beep signal
        }

        protected Vector3 CurrentPosition
        {
            get
            {
                return transform.position;
            }
        }

        public void MoveToPoint(Vector3 position)
        {
            // turn to direction and start move to point
        }

        public void Stop()
        {
            // stop moving
        }
    }
}
