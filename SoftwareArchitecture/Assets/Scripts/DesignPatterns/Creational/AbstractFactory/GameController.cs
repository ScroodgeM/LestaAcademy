//this empty line for UTF-8 BOM header
using System.Collections.Generic;

namespace LestaAcademyDemo.DesignPatterns.Creational.AbstractFactory
{
    public class GameController
    {
        private IFactory factory;

        private readonly List<IUnit> units = new List<IUnit>();

        public GameController(IFactory factory)
        {
            this.factory = factory;
        }

        public void CreateArmy()
        {
            CreateUnits(UnitType.Soldier, WeaponType.AssaultRifle, 100);
            CreateUnits(UnitType.Medic, WeaponType.Pistol, 5);
            CreateUnits(UnitType.Sniper, WeaponType.SniperRifle, 20);
        }

        private void CreateUnits(UnitType unitType, WeaponType weaponType, int count)
        {
            for (int i = 0; i < count; i++)
            {
                IUnit unit = factory.CreateUnit(unitType);

                IWeapon weapon = factory.CreateWeapon(weaponType);
                unit.AttachWeapon(weapon);

                units.Add(unit);
            }
        }
    }
}
