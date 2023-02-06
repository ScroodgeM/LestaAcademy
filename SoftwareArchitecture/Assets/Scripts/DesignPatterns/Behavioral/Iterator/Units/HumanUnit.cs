//this empty line for UTF-8 BOM header
using System;

namespace LestaAcademyDemo.DesignPatterns.Behavioral.Iterator.Units
{
    public class HumanUnit : IUnit
    {
        public bool IsMovable => throw new NotImplementedException();

        public UnitType UnitType => UnitType.Human;

        public string GetAlliance()
        {
            throw new NotImplementedException();
        }
    }
}
