
namespace WGADemo.DesignPrinciples.EncapsulateWhatChanges.Correct
{
    public enum UnitType
    {
        Healer, Builder, Soldier,
    }

    public interface ISkill
    {
        void UseSkill(Unit unit);
    }

    public class Unit
    {
        private ISkill skill;

        public Unit(UnitType unitType)
        {
            switch (unitType)
            {
                case UnitType.Healer:
                    skill = new HealSkill();
                    break;

                case UnitType.Builder:
                    skill = new BuildSkill();
                    break;

                case UnitType.Soldier:
                    skill = new AttackSkill();
                    break;
            }
        }

        public void UseSkill()
        {
            skill.UseSkill(this);
        }
    }

    public class UnitBehaviour
    {
        public void UseSkill(Unit unit)
        {
            unit.UseSkill();
        }
    }

    public class HealSkill : ISkill
    {
        public void UseSkill(Unit unit) { }
    }

    public class BuildSkill : ISkill
    {
        public void UseSkill(Unit unit) { }
    }

    public class AttackSkill : ISkill
    {
        public void UseSkill(Unit unit) { }
    }
}
