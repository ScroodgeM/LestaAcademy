//this empty line for UTF-8 BOM header

namespace LestaAcademyDemo.DesignPatterns.Creational.Singleton
{
    public interface IFactory
    {
        IUnit CreateUnit(UnitType unitType);
        IWeapon CreateWeapon(WeaponType weaponType);
    }
}
