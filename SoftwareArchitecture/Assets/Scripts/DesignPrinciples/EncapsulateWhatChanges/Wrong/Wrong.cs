//this empty line for UTF-8 BOM header

namespace LestaAcademyDemo.DesignPrinciples.EncapsulateWhatChanges.Wrong
{
    public enum UnitType
    {
        Healer,
        Builder,
        Soldier,
    }

    public class Unit
    {
        private UnitType unitType;

        public UnitType GetUnitType() => unitType;

        public void UseHealSkill()
        {
            /* some actions here */
        }

        public void UseBuildSkill()
        {
            /* some actions here */
        }

        public void UseAttackSkill()
        {
            /* some actions here */
        }
    }

    public class UnitBehaviour
    {
        public void UseSkill(Unit unit)
        {
            switch (unit.GetUnitType())
            {
                case UnitType.Healer:
                    unit.UseHealSkill();
                    break;

                case UnitType.Builder:
                    unit.UseBuildSkill();
                    break;

                case UnitType.Soldier:
                    unit.UseAttackSkill();
                    break;
            }
        }
    }
}
