
using System;

namespace LestaAcademyDemo.DesignPatterns.Behavioral.Iterator
{
    public class BuildingUnit : IUnit
    {
        public bool IsMovable => throw new NotImplementedException();

        public UnitType UnitType => UnitType.Building;

        public string GetAlliance()
        {
            throw new NotImplementedException();
        }
    }
}
