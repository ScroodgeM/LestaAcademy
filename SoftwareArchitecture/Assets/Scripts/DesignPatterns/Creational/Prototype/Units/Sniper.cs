﻿
using System;

namespace WGADemo.DesignPatterns.Creational.Prototype.Units
{
    public class Sniper : IUnit
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

        public void AttackArmor(IArmor armor)
        {
            throw new NotImplementedException();
        }
    }
}