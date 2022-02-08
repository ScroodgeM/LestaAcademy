
namespace WGADemo.DesignPatterns.Structural.Decorator
{
    public class UnitFactory
    {
        public IUnit GetDecoratedUnit()
        {
            IUnit unit = null; // replace it with base unit implementation

            unit = new DamagePlayerOnHitUnit().SetUnit(unit);

            unit = new ExplodeOnDeathUnit(2f).SetUnit(unit);

            unit = new KnockbackOnPushUnit().SetUnit(unit);

            unit = new ExplodeOnDeathUnit(25f).SetUnit(unit);

            return unit;
        }
    }
}
