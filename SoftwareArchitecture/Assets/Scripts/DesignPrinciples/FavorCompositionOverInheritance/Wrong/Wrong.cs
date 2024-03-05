//this empty line for UTF-8 BOM header

namespace LestaAcademyDemo.DesignPrinciples.FavorCompositionOverInheritance.Wrong
{
    public interface IUnit
    {
        void Move();
        void Attack();
    }

    public abstract class Unit : IUnit
    {
        public abstract void Move();
        public abstract void Attack();
    }

    public abstract class RangeUnit : Unit
    {
        public override void Attack()
        {
            // range attack here
        }
    }

    public abstract class MeleeUnit : Unit
    {
        public override void Attack()
        {
            // melee attack here
        }
    }

    public class RangeUnitOnLegs : RangeUnit
    {
        public override void Move()
        {
            //move with legs
        }
    }

    public class RangeUnitOnWheels : RangeUnit
    {
        public override void Move()
        {
            //move with wheels
        }
    }

    public class MeleeUnitOnLegs : MeleeUnit
    {
        public override void Move()
        {
            //move with legs
        }
    }

    public class MeleeUnitOnWheels : MeleeUnit
    {
        public override void Move()
        {
            //move with wheels
        }
    }
}
