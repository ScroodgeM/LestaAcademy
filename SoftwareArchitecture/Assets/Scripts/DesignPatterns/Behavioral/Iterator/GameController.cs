//this empty line for UTF-8 BOM header

using System;
using System.Collections.Generic;
using LestaAcademyDemo.DesignPatterns.Behavioral.Iterator.Units;

namespace LestaAcademyDemo.DesignPatterns.Behavioral.Iterator
{
    // encapsulate concrete implementation
    public class GameController : IGameController
    {
        private List<HumanUnit> humans;
        private Dictionary<string, BuildingUnit> buildings;
        private BuildingUnit[] immortalTurrets;
        private BossUnit boss;

        public IEnumerable<IUnit> GetAllUnitsOfType(UnitType unitType)
        {
            switch (unitType)
            {
                case UnitType.Human:
                    return humans;

                case UnitType.Building:
                    return buildings.Values;

                case UnitType.ImmortalTurret:
                    return immortalTurrets;

                case UnitType.Boss:
                    return new IUnit[] { boss };
            }

            throw new NotSupportedException();
        }

        public IEnumerable<IUnit> GetAllUnits()
        {
            foreach (HumanUnit humanUnit in humans)
            {
                yield return humanUnit;
            }

            foreach (BuildingUnit buildingUnit in buildings.Values)
            {
                yield return buildingUnit;
            }

            foreach (BuildingUnit buildingUnit in immortalTurrets)
            {
                yield return buildingUnit;
            }

            yield return boss;
        }

        public IEnumerable<IUnit> GetAllUnitsOfAlliance(string allianceName)
        {
            foreach (IUnit unit in GetAllUnits())
            {
                if (unit.GetAlliance() == allianceName)
                {
                    yield return unit;
                }
            }
        }
    }
}
