
using System;
using System.Collections.Generic;

namespace WGADemo.DesignPrinciples.InterfaceSegregationPrinciple.Wrong
{
    public class GameController
    {
        private List<IUnit> units;

        public void MoveArmy()
        {
            foreach (IUnit unit in units)
            {
                try
                {
                    unit.Move();
                }
                catch (NotSupportedException)
                {
                    // don't move =)
                }
            }
        }
    }

    public interface IUnit
    {
        void Attack();
        void Move();
    }

    public class Soldier : IUnit
    {
        public void Attack()
        {
            // do attack
        }

        public void Move()
        {
            // run
        }
    }

    public class SniperTower : IUnit
    {
        public void Attack()
        {
            // do attack
        }

        public void Move()
        {
            throw new NotSupportedException();
        }
    }
}
