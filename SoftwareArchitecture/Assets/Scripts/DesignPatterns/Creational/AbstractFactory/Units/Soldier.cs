
using System;

namespace WGADemo.DesignPatterns.AbstractFactory.Units
{
    public class Soldier : IUnit
    {
        public void UseSkill()
        {
            throw new NotImplementedException();
        }

        public void Move()
        {
            throw new NotImplementedException();
        }

        public void AttachWeapon(IWeapon weapon)
        {
            throw new NotImplementedException();
        }
    }
}
