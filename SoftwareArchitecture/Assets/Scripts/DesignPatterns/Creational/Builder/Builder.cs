
using System;
using WGADemo.DesignPatterns.Creational.Builder.Armors;
using WGADemo.DesignPatterns.Creational.Builder.Units;
using WGADemo.DesignPatterns.Creational.Builder.Weapons;

namespace WGADemo.DesignPatterns.Creational.Builder
{
    public static class Builder
    {
        public static IUnit CreateUnit(UnitType unitType, UnitRank unitRank)
        {
            switch (unitType)
            {
                case UnitType.Medic:
                    return CreateMedic(unitRank);

                case UnitType.Sniper:
                    return CreateSniper(unitRank);

                case UnitType.Soldier:
                    return CreateSoldier(unitRank);
            }

            throw new NotSupportedException($"unit of type {unitType} and rank {unitRank} not supported");
        }

        private static Soldier CreateSoldier(UnitRank unitRank)
        {
            Soldier soldierUnit = new Soldier();

            switch (unitRank)
            {
                case UnitRank.Novice:
                    soldierUnit.AttachWeapon(new AssaultRifle());
                    soldierUnit.AttachArmor(new LightArmor());
                    return soldierUnit;

                case UnitRank.Veteran:
                    soldierUnit.AttachWeapon(new AssaultRifle());
                    soldierUnit.AttachWeapon(new Pistol());
                    soldierUnit.AttachArmor(new HeavyArmor());
                    return soldierUnit;
            }

            throw new NotSupportedException($"unit of type Soldier and rank {unitRank} not supported");
        }

        private static Sniper CreateSniper(UnitRank unitRank)
        {
            Sniper sniperUnit = new Sniper();

            switch (unitRank)
            {
                case UnitRank.Novice:
                    sniperUnit.AttachWeapon(new SniperRifle());
                    return sniperUnit;

                case UnitRank.Veteran:
                    sniperUnit.AttachWeapon(new SniperRifle());
                    sniperUnit.AttachWeapon(new Pistol());
                    sniperUnit.AttachArmor(new LightArmor());
                    return sniperUnit;
            }

            throw new NotSupportedException($"unit of type Sniper and rank {unitRank} not supported");
        }

        private static Medic CreateMedic(UnitRank unitRank)
        {
            Medic medicUnit = new Medic();

            switch (unitRank)
            {
                case UnitRank.Novice:
                    medicUnit.AttachWeapon(new Pistol());
                    return medicUnit;

                case UnitRank.Veteran:
                    medicUnit.AttachWeapon(new AssaultRifle());
                    medicUnit.AttachArmor(new LightArmor());
                    return medicUnit;
            }

            throw new NotSupportedException($"unit of type Medic and rank {unitRank} not supported");
        }
    }
}
