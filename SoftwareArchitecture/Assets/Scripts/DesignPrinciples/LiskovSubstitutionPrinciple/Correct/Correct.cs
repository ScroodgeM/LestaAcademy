
using System.Collections.Generic;

namespace WGADemo.DesignPrinciples.LiskovSubstitutionPrinciple.Correct
{
    public class GameController
    {
        private List<Unit> playerArmy;

        public void MoveArmy()
        {
            foreach (Unit unit in playerArmy)
            {
                if (unit is IMovableUnit movableUnit)
                {
                    movableUnit.Move();
                }
            }
        }
    }

    public interface IMovableUnit
    {
        void Move();
    }

    public abstract class Unit
    {
        public abstract void Attack();
    }

    public abstract class UnitWithSniperAttack : Unit
    {
        public override void Attack()
        {
            // sniper attack here
        }
    }

    public class SniperUnit : UnitWithSniperAttack, IMovableUnit
    {
        public void Move()
        {
            // simple move here
        }
    }

    public class SniperTower : UnitWithSniperAttack
    {
        public override void Attack()
        {
            base.Attack();
            base.Attack();
            base.Attack();
        }
    }
}
