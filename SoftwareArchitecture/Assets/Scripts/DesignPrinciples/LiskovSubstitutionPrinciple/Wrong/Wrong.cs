//this empty line for UTF-8 BOM header

using System;
using System.Collections.Generic;

namespace LestaAcademyDemo.DesignPrinciples.LiskovSubstitutionPrinciple.Wrong
{
    public class GameController
    {
        private List<Unit> playerArmy;

        public void MoveArmy()
        {
            foreach (Unit unit in playerArmy)
            {
                unit.Move();
            }
        }
    }

    public abstract class Unit
    {
        public abstract void Attack();
        public abstract void Move();
    }

    public class SniperUnit : Unit
    {
        public override void Attack()
        {
            // sniper attack here
        }

        public override void Move()
        {
            // simple move here
        }
    }

    public class SniperTower : SniperUnit
    {
        public override void Attack()
        {
            base.Attack();
            base.Attack();
            base.Attack();
        }

        public override void Move()
        {
            throw new NotSupportedException();
        }
    }
}
