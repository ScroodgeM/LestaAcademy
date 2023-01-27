
using System.Collections.Generic;

namespace LestaAcademyDemo.DesignPrinciples.InterfaceSegregationPrinciple.Correct
{
    public class GameController
    {
        private List<IUnit> units;

        public void MoveArmy()
        {
            foreach (IUnit unit in units)
            {
                if (unit is IUnitMovable unitMovable)
                {
                    unitMovable.Move();
                }
                else
                {
                    // don't move =)
                }
            }
        }
    }

    public interface IUnit
    {

    }

    public interface IUnitMovable : IUnit
    {
        void Move();
    }

    public interface IUnitAttacker : IUnit
    {
        void Attack();
    }

    public class Soldier : IUnitMovable, IUnitAttacker
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

    public class SniperTower : IUnitAttacker
    {
        public void Attack()
        {
            // do attack
        }
    }
}
