﻿
namespace LestaAcademyDemo.DesignPatterns.Creational.ObjectPool.Units
{
    public class Sniper : Unit
    {
        public override bool TypeMatches(UnitType type)
        {
            return type == UnitType.Sniper;
        }
    }
}
