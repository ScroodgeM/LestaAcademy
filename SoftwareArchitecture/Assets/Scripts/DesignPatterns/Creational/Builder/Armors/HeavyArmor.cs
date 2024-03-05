﻿//this empty line for UTF-8 BOM header

namespace LestaAcademyDemo.DesignPatterns.Creational.Builder.Armors
{
    public class HeavyArmor : IArmor
    {
        public int ProcessIncomingDamage(int incomingDamage)
        {
            /* some actions here */
            return incomingDamage;
        }
    }
}
