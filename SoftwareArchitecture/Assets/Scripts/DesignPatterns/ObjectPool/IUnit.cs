
namespace WGADemo.DesignPatterns.ObjectPool
{
    public interface IUnit : IObjectPoolMember<UnitType>
    {
        void Move();
        void UseSkill();
    }
}
