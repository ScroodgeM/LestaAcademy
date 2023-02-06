//this empty line for UTF-8 BOM header
using System;
using System.Collections.Generic;
using LestaAcademyDemo.DesignPatterns.Creational.ObjectPool.Units;

namespace LestaAcademyDemo.DesignPatterns.Creational.ObjectPool
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
                unit.OnDeath += OnUnitDeath;
                units.Add(unit);
            }
        }

        private void OnUnitDeath(IUnit unit)
        {
            unit.OnDeath -= OnUnitDeath;
            units.Remove(unit);
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
