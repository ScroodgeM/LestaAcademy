//this empty line for UTF-8 BOM header
using System.Collections.Generic;

namespace LestaAcademyDemo.DesignPatterns.Creational.Prototype
{
    public class GameController
    {
        private PrototypeSelector prototypeSelector;

        private readonly List<IUnit> units = new List<IUnit>();

        public void CreateArmy(UnitRank unitRank)
        {
            CreateUnits(UnitType.Soldier, unitRank, 100);
            CreateUnits(UnitType.Medic, unitRank, 5);
            CreateUnits(UnitType.Sniper, unitRank, 20);
        }

        private void CreateUnits(UnitType unitType, UnitRank unitRank, int count)
        {
            for (int i = 0; i < count; i++)
            {
                IUnit unit = prototypeSelector.CreateUnit(unitType, unitRank);
                units.Add(unit);
            }
        }
    }
}
