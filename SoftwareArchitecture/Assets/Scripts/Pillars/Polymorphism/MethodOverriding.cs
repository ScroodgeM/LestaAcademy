
using System.Collections.Generic;

namespace LestaAcademyDemo.Pillars.Polymorphism
{
    public abstract class UnitBase
    {
        public void HandleImcomingDamage(/*some data about damage here*/)
        {
            //some magic here
        }

        public abstract void Attack();
    }

    public class MeleeUnit : UnitBase
    {
        public override void Attack()
        {
            // cut enemy using a sword
        }
    }

    public class RangeUnit : UnitBase
    {
        public override void Attack()
        {
            // fire using bow and arrows
        }
    }

    public class GameController
    {
        private List<UnitBase> army;

        public GameController()
        {
            army = new List<UnitBase>();
            army.Add(new RangeUnit());
            army.Add(new MeleeUnit());
        }

        public void ArmyAttack()
        {
            foreach (UnitBase unit in army)
            {
                unit.Attack();
            }
        }
    }
}
