//this empty line for UTF-8 BOM header

namespace LestaAcademyDemo.DesignPrinciples.EncapsulateWhatChanges.Correct
{
    public enum UnitType
    {
        Healer,
        Builder,
        Soldier,
    }

    public interface ISkill
    {
        void UseSkill(Unit unit);
    }

    public class Unit
    {
        private readonly ISkill skill;

        public Unit(UnitType unitType)
        {
            skill = GetSkillByUnitType(unitType);
        }

        public void UseSkill()
        {
            skill.UseSkill(this);
        }

        private static ISkill GetSkillByUnitType(UnitType unitType)
        {
            switch (unitType)
            {
                case UnitType.Healer: return new HealSkill();
                case UnitType.Builder: return new BuildSkill();
                case UnitType.Soldier: return new AttackSkill();
                default:
                    /* print error about missing skill */
                    return default;
            }
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
        public void UseSkill(Unit unit)
        {
            /* some actions here */
        }
    }

    public class BuildSkill : ISkill
    {
        public void UseSkill(Unit unit)
        {
            /* some actions here */
        }
    }

    public class AttackSkill : ISkill
    {
        public void UseSkill(Unit unit)
        {
            /* some actions here */
        }
    }
}
