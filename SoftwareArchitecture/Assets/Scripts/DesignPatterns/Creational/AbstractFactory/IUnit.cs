
namespace LestaAcademyDemo.DesignPatterns.Creational.AbstractFactory
{
    public interface IUnit
    {
        void AttachWeapon(IWeapon weapon);

        void Move();
        void UseSkill();
    }
}
