namespace WGADemo.DesignPatterns.Structural.Decorator
{
    public abstract class EnemyUnitDecorator : IEnemyUnit
    {
        private IEnemyUnit enemyUnit;

        public IEnemyUnit SetEnemyUnit(IEnemyUnit enemyUnit)
        {
            this.enemyUnit = enemyUnit;
            return this;
        }

        public virtual void Damage(int damagePoints) => enemyUnit.Damage(damagePoints);

        public virtual void Kill() => enemyUnit.Kill();

        public virtual void Push(int forcePoint) => enemyUnit.Push(forcePoint);
    }
}
