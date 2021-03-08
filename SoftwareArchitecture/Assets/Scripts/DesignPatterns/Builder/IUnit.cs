
namespace WGADemo.DesignPatterns.Builder
{
    public interface IUnit
    {
        void AttachWeapon(IWeapon weapon);
        void AttackArmor(IArmor armor);

        void Move();
        void UseSkill();
    }
}
