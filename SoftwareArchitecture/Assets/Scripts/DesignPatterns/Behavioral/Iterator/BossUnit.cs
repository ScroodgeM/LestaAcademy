
using System;

namespace LestaAcademyDemo.DesignPatterns.Behavioral.Iterator
{
    public class BossUnit : IUnit
    {
        public bool IsMovable => throw new NotImplementedException();

        public UnitType UnitType => UnitType.Boss;

        public string GetAlliance()
        {
            throw new NotImplementedException();
        }
    }
}
