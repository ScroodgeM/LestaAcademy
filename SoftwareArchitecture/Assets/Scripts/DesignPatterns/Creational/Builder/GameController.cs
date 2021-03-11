using System;
using System.Collections.Generic;

namespace WGADemo.DesignPatterns.Builder
{
    public class GameController
    {
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
                IUnit unit = CreateUnit(unitType, unitRank);
                units.Add(unit);
            }
        }

        private static IUnit CreateUnit(UnitType unitType, UnitRank unitRank)
        {
            switch (unitType)
            {
                case UnitType.Medic:
                    Units.Medic medic = Builder.CreateMedic(unitRank);
                    return medic;

                case UnitType.Sniper:
                    Units.Sniper sniper = Builder.CreateSniper(unitRank);
                    return sniper;

                case UnitType.Soldier:
                    Units.Soldier soldier = Builder.CreateSoldier(unitRank);
                    return soldier;
            }

            throw new NotSupportedException($"unit of type {unitType} and rank {unitRank} not supported");
        }
    }
}
