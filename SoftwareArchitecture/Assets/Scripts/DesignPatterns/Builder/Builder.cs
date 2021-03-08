using System;
using WGADemo.DesignPatterns.Builder.Armors;
using WGADemo.DesignPatterns.Builder.Units;
using WGADemo.DesignPatterns.Builder.Weapons;

namespace WGADemo.DesignPatterns.Builder
{
    public class Builder
    {
        public IUnit CreateUnit(UnitType unitType, UnitRank unitRank)
        {
            IUnit unit;

            switch (unitType)
            {
                case UnitType.Medic:
                    unit = new Medic();
                    switch (unitRank)
                    {
                        case UnitRank.Novice:
                            unit.AttachWeapon(new Pistol());
                            return unit;

                        case UnitRank.Veteran:
                            unit.AttachWeapon(new AssaultRifle());
                            unit.AttackArmor(new LightArmor());
                            return unit;
                    }
                    break;

                case UnitType.Sniper:
                    unit = new Sniper();
                    switch (unitRank)
                    {
                        case UnitRank.Novice:
                            unit.AttachWeapon(new SniperRifle());
                            return unit;

                        case UnitRank.Veteran:
                            unit.AttachWeapon(new SniperRifle());
                            unit.AttachWeapon(new Pistol());
                            unit.AttackArmor(new LightArmor());
                            return unit;
                    }
                    break;

                case UnitType.Soldier:
                    unit = new Soldier();
                    switch (unitRank)
                    {
                        case UnitRank.Novice:
                            unit.AttachWeapon(new AssaultRifle());
                            unit.AttackArmor(new LightArmor());
                            return unit;

                        case UnitRank.Veteran:
                            unit.AttachWeapon(new AssaultRifle());
                            unit.AttachWeapon(new Pistol());
                            unit.AttackArmor(new HeavyArmor());
                            return unit;
                    }
                    break;
            }

            throw new NotSupportedException($"unit of type {unitType} and rank {unitRank} not supported");
        }
    }
}
