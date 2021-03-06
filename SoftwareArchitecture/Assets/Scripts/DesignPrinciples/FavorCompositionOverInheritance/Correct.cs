namespace WGADemo.DesignPrinciples.FavorCompositionOverInheritance.Correct
{
    public interface IUnit
    {
        IUnitMover Mover { get; }
        IUnitAttacker Attacker { get; }
    }

    public interface IUnitMover
    {
        void Move();
    }

    public interface IUnitAttacker
    {
        void Attack();
    }

    public class LegMover : IUnitMover
    {
        public void Move()
        {
            //move with legs
        }
    }

    public class WheelsMover : IUnitMover
    {
        public void Move()
        {
            //move with wheels
        }
    }

    public class MeleeAttacker : IUnitAttacker
    {
        public void Attack()
        {
            // range attack here
        }
    }

    public class RangeAttacker : IUnitAttacker
    {
        public void Attack()
        {
            // melee attack here
        }
    }

    public abstract class Unit : IUnit
    {
        public IUnitMover Mover { get; protected set; }
        public IUnitAttacker Attacker { get; protected set; }
    }

    public class RangeUnitOnLegs : Unit
    {
        public RangeUnitOnLegs()
        {
            Mover = new LegMover();
            Attacker = new RangeAttacker();
        }
    }

    public class RangeUnitOnWheels : Unit
    {
        public RangeUnitOnWheels()
        {
            Mover = new WheelsMover();
            Attacker = new RangeAttacker();
        }
    }

    public class MeleeUnitOnLegs : Unit
    {
        public MeleeUnitOnLegs()
        {
            Mover = new LegMover();
            Attacker = new MeleeAttacker();
        }
    }

    public class MeleeUnitOnWheels : Unit
    {
        public MeleeUnitOnWheels()
        {
            Mover = new WheelsMover();
            Attacker = new MeleeAttacker();
        }
    }
}
