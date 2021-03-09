
namespace WGADemo.DesignPatterns.Singleton
{
    public interface IFactory
    {
        IUnit CreateUnit(UnitType unitType);
        IWeapon CreateWeapon(WeaponType weaponType);
    }
}
