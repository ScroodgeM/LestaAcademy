//this empty line for UTF-8 BOM header

namespace LestaAcademyDemo.DesignPrinciples.FavorCompositionOverInheritance.Wrong
{
    public interface IUnit
    {
        void Move();
        void Attack();
    }

    public abstract class Unit
    {

    }

    public abstract class RangeUnit : Unit
    {
        public void Attack()
        {
            // range attack here
        }
    }

    public abstract class MeleeUnit : Unit
    {
        public void Attack()
        {
            // melee attack here
        }
    }

    public class RangeUnitOnLegs : RangeUnit, IUnit
    {
        public void Move()
        {
            //move with legs
        }
    }

    public class RangeUnitOnWheels : RangeUnit, IUnit
    {
        public void Move()
        {
            //move with wheels
        }
    }

    public class MeleeUnitOnLegs : MeleeUnit, IUnit
    {
        public void Move()
        {
            //move with legs
        }
    }

    public class MeleeUnitOnWheels : MeleeUnit, IUnit
    {
        public void Move()
        {
            //move with wheels
        }
    }
}
