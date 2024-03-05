//this empty line for UTF-8 BOM header

using System;
using System.Collections.Generic;
using LestaAcademyDemo.DesignPatterns.Creational.FactoryMethod.Units;

namespace LestaAcademyDemo.DesignPatterns.Creational.FactoryMethod
{
    public class GameController
    {
        private readonly List<IUnit> units = new List<IUnit>();

        public void CreateArmy()
        {
            CreateUnits(UnitType.Soldier, 100);
            CreateUnits(UnitType.Medic, 5);
            CreateUnits(UnitType.Sniper, 20);
        }

        private void CreateUnits(UnitType unitType, int count)
        {
            for (int i = 0; i < count; i++)
            {
                IUnit unit = CreateUnit(unitType);
                units.Add(unit);
            }
        }

        private static IUnit CreateUnit(UnitType unitType)
        {
            switch (unitType)
            {
                case UnitType.Medic:
                    return new Medic();

                case UnitType.Sniper:
                    return new Sniper();

                case UnitType.Soldier:
                    return new Soldier();

                default:
                    throw new NotSupportedException($"unit of type {unitType} not supported");
            }
        }
    }
}
