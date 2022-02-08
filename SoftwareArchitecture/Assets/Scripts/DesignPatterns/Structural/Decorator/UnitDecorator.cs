
namespace WGADemo.DesignPatterns.Structural.Decorator
{
    public abstract class UnitDecorator : IUnit
    {
        private IUnit unit;

        public IUnit SetUnit(IUnit unit)
        {
            this.unit = unit;
            return this;
        }

        public virtual void Damage(int damagePoints) => unit.Damage(damagePoints);

        public virtual void Kill() => unit.Kill();

        public virtual void Push(int forcePoint) => unit.Push(forcePoint);
    }
}
