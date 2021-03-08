
namespace WGADemo.DesignPatterns.AbstractFactory
{
    public interface IUnit
    {
        void AttachWeapon(IWeapon weapon);

        void Move();
        void UseSkill();
    }
}
