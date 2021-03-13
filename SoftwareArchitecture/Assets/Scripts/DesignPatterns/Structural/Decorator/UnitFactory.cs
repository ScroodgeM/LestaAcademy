
namespace WGADemo.DesignPatterns.Structural.Decorator
{
    public class UnitFactory
    {
        public IEnemyUnit GetDecoratedUnit()
        {
            IEnemyUnit enemyUnit = null; // replace it with base enemy unit implementation

            enemyUnit = new DamagePlayerOnHitEnemyUnit().SetEnemyUnit(enemyUnit);

            enemyUnit = new ExplodeOnDeathEnemyUnit(2f).SetEnemyUnit(enemyUnit);

            enemyUnit = new KnockbackOnPushEnemyUnit().SetEnemyUnit(enemyUnit);

            enemyUnit = new ExplodeOnDeathEnemyUnit(25f).SetEnemyUnit(enemyUnit);

            return enemyUnit;
        }
    }
}
