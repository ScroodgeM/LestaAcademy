
using System;

namespace WGADemo.DesignPatterns.Behavioral.Iterator
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
