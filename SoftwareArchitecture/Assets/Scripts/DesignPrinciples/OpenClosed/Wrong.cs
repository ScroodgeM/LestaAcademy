
namespace WGADemo.DesignPrinciples.OpenClosed.Wrong
{
    public enum DamageType
    {
        Physical, Magic, Fire, Ice
    }

    public class Armor
    {
        public float physicalAbsorb;
        public float physicalResist;
        public float fireResist;
        public float iceResist;
    }

    public class Damage
    {
        public DamageType type;
        public float points;
    }

    public class Unit
    {
        private float health;
        private Armor armor;

        public void Damage(Damage damage)
        {
            float actualDamage;

            switch (damage.type)
            {
                case DamageType.Physical:
                    actualDamage = (damage.points - armor.physicalAbsorb) * (1f - armor.physicalResist);
                    break;
                case DamageType.Magic:
                    actualDamage = damage.points;
                    break;
                case DamageType.Fire:
                    actualDamage = damage.points * (1f - armor.fireResist);
                    break;
                case DamageType.Ice:
                    actualDamage = damage.points * (1f - armor.iceResist);
                    break;
                default:
                    return;
            }

            if (actualDamage > 0f)
            {
                health -= actualDamage;
            }
        }
    }
}
