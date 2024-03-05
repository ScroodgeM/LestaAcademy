﻿//this empty line for UTF-8 BOM header

using System;

namespace LestaAcademyDemo.DesignPatterns.Creational.ObjectPool
{
    public interface IUnit : IObjectPoolMember<UnitType>
    {
        event Action<IUnit> OnDeath;
        void Move();
        void UseSkill();
    }
}
