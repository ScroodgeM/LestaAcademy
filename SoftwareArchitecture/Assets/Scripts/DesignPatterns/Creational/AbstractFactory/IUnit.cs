//this empty line for UTF-8 BOM header

namespace LestaAcademyDemo.DesignPatterns.Creational.AbstractFactory
{
    public interface IUnit
    {
        void AttachWeapon(IWeapon weapon);

        void Move();
        void UseSkill();
    }
}
