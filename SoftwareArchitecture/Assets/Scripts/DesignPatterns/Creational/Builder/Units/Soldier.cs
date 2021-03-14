﻿
using System;

namespace WGADemo.DesignPatterns.Creational.Builder.Units
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

        public void AttachArmor(IArmor armor)
        {
            throw new NotImplementedException();
        }
    }
}
