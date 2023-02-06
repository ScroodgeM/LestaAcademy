﻿//this empty line for UTF-8 BOM header
using System;
using UnityEngine;
using LestaAcademyDemo.DesignPatterns.Creational.Singleton.Units;
using LestaAcademyDemo.DesignPatterns.Creational.Singleton.Weapons;

namespace LestaAcademyDemo.DesignPatterns.Creational.Singleton
{
    public class Factory : MonoBehaviour, IFactory
    {
        public IUnit CreateUnit(UnitType unitType)
        {
            switch (unitType)
            {
                case UnitType.Medic:
                    return new Medic();

                case UnitType.Sniper:
                    return new Sniper();

                case UnitType.Soldier:
                    return new Soldier();

                default:
                    throw new NotSupportedException($"unit of type {unitType} not supported");
            }
        }

        public IWeapon CreateWeapon(WeaponType weaponType)
        {
            switch (weaponType)
            {
                case WeaponType.Pistol:
                    return new Pistol();

                case WeaponType.AssaultRifle:
                    return new AssaultRifle();

                case WeaponType.SniperRifle:
                    return new SniperRifle();

                default:
                    throw new NotSupportedException($"weapon of type {weaponType} not supported");
            }
        }
    }
}
