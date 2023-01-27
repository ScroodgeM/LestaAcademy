
using System;
using System.Collections.Generic;
using LestaAcademyDemo.DesignPatterns.Creational.Multiton.Units;

namespace LestaAcademyDemo.DesignPatterns.Creational.Multiton
{
    public class GameController
    {
        private readonly ObjectPool<IUnit, UnitType> unitsPool = new ObjectPool<IUnit, UnitType>(CreateUnit);

        private readonly List<IUnit> units = new List<IUnit>();

        public void CreateUnits(UnitType unitType, int count)
        {
            for (int i = 0; i < count; i++)
            {
                IUnit unit = unitsPool.GetObject(unitType);
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
