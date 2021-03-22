
using System;

namespace WGADemo.DesignPatterns.Behavioral.Iterator
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
