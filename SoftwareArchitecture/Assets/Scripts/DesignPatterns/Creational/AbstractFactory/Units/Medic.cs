﻿
using System;

namespace WGADemo.DesignPatterns.Creational.AbstractFactory.Units
{
    public class Medic : IUnit
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
