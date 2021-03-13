
namespace WGADemo.DesignPatterns.Creational.ObjectPool
{
    public interface IUnit : IObjectPoolMember<UnitType>
    {
        void Move();
        void UseSkill();
    }
}
