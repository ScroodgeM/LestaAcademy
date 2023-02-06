//this empty line for UTF-8 BOM header

namespace LestaAcademyDemo.DesignPrinciples.OpenClosed.Correct
{
    public class Armor
    {
        public float physicalAbsorb;
        public float physicalResist;
        public float fireResist;
        public float iceResist;
    }

    public abstract class Damage
    {
        protected float points;

        public abstract float GetActualDamage(Armor armor);
    }

    public class PhysicalDamage : Damage
    {
        public override float GetActualDamage(Armor armor) => (points - armor.physicalAbsorb) * (1f - armor.physicalResist);
    }

    public class MagicDamage : Damage
    {
        public override float GetActualDamage(Armor armor) => points;
    }

    public class FireDamage : Damage
    {
        public override float GetActualDamage(Armor armor) => points * (1f - armor.fireResist);
    }

    public class IceDamage : Damage
    {
        public override float GetActualDamage(Armor armor) => points * (1f - armor.iceResist);
    }

    public class Unit
    {
        private float health;
        private Armor armor;

        public void Damage(Damage damage)
        {
            float actualDamage = damage.GetActualDamage(armor);

            if (actualDamage > 0f)
            {
                health -= actualDamage;
            }
        }
    }
}
