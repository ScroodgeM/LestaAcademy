
using System.Collections.Generic;
using UnityEngine;

namespace WGADemo.DesignPatterns.Structural.Composite
{
    public class CompositeUnit : IUnit
    {
        private readonly List<IUnit> units = new List<IUnit>();

        public CompositeUnit(IEnumerable<IUnit> units)
        {
            this.units.AddRange(units);
        }

        public bool ArrivedToDestination
        {
            get
            {
                foreach (IUnit unit in units)
                {
                    if (unit.ArrivedToDestination == false)
                    {
                        return false;
                    }
                }

                return true;
            }
        }

        public void Fire(Vector3 aimPoint)
        {
            foreach (IUnit unit in units)
            {
                unit.Fire(aimPoint);
            }
        }

        public float GetHealth()
        {
            float health = 0f;

            foreach (IUnit unit in units)
            {
                health += unit.GetHealth();
            }

            return health;
        }
    }
}
