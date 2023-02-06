namespace LestaAcademyDemo.DesignPatterns.Creational.AbstractFactory
{
    public interface IFactory
    {
        IUnit CreateUnit(UnitType unitType);
        IWeapon CreateWeapon(WeaponType weaponType);
    }
}
