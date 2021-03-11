using System;
using WGADemo.DesignPatterns.Builder.Armors;
using WGADemo.DesignPatterns.Builder.Units;
using WGADemo.DesignPatterns.Builder.Weapons;

namespace WGADemo.DesignPatterns.Builder
{
    public static class Builder
    {
        public static Soldier CreateSoldier(UnitRank unitRank)
        {
            Soldier soldierUnit = new Soldier();

            switch (unitRank)
            {
                case UnitRank.Novice:
                    soldierUnit.AttachWeapon(new AssaultRifle());
                    soldierUnit.AttackArmor(new LightArmor());
                    return soldierUnit;

                case UnitRank.Veteran:
                    soldierUnit.AttachWeapon(new AssaultRifle());
                    soldierUnit.AttachWeapon(new Pistol());
                    soldierUnit.AttackArmor(new HeavyArmor());
                    return soldierUnit;
            }

            throw new NotSupportedException($"unit of type Soldier and rank {unitRank} not supported");
        }

        public static Sniper CreateSniper(UnitRank unitRank)
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
                    sniperUnit.AttackArmor(new LightArmor());
                    return sniperUnit;
            }

            throw new NotSupportedException($"unit of type Sniper and rank {unitRank} not supported");
        }

        public static Medic CreateMedic(UnitRank unitRank)
        {
            Medic medicUnit = new Medic();

            switch (unitRank)
            {
                case UnitRank.Novice:
                    medicUnit.AttachWeapon(new Pistol());
                    return medicUnit;

                case UnitRank.Veteran:
                    medicUnit.AttachWeapon(new AssaultRifle());
                    medicUnit.AttackArmor(new LightArmor());
                    return medicUnit;
            }

            throw new NotSupportedException($"unit of type Medic and rank {unitRank} not supported");
        }
    }
}
