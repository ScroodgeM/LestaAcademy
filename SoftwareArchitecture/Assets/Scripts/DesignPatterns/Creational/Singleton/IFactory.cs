namespace LestaAcademyDemo.DesignPatterns.Creational.Singleton
{
    public interface IFactory
    {
        IUnit CreateUnit(UnitType unitType);
        IWeapon CreateWeapon(WeaponType weaponType);
    }
}
