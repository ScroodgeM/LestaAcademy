//this empty line for UTF-8 BOM header
using System;

namespace LestaAcademyDemo.DesignPatterns.Behavioral.Iterator.Units
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
