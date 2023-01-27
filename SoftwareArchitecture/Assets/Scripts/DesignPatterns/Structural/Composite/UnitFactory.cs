
using System;
using System.Collections.Generic;

namespace LestaAcademyDemo.DesignPatterns.Structural.Composite
{
    public static class UnitFactory
    {
        public static IUnit CreateSingleUnit()
        {
            throw new NotImplementedException();
        }

        public static IUnit CreateSquadUnit()
        {
            List<IUnit> squadUnits = new List<IUnit>();

            for (int i = 0; i < 7; i++)
            {
                squadUnits.Add(CreateSingleUnit());
            }

            return new CompositeUnit(squadUnits);
        }

        public static IUnit CreatePlatoonUnit()
        {
            List<IUnit> platoonUnits = new List<IUnit>();

            for (int i = 0; i < 4; i++)
            {
                platoonUnits.Add(CreateSquadUnit());
            }

            return new CompositeUnit(platoonUnits);
        }
    }
}
