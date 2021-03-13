
namespace WGADemo.DesignPatterns.Structural.Decorator
{
    public interface IEnemyUnit
    {
        void Damage(int damagePoints);
        void Push(int forcePoint);
        void Kill();
    }
}
