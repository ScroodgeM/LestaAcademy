
namespace WGADemo.DesignPrinciples.EncapsulateWhatChanges.Correct
{
    public enum UnitType
    {
        Healer, Builder, Soldier,
    }

    public class Unit
    {
        private UnitType unitType;

        public void UseSkill()
        {
            switch (unitType)
            {
                case UnitType.Healer:
                    UseHealSkill();
                    break;

                case UnitType.Builder:
                    UseBuildSkill();
                    break;

                case UnitType.Soldier:
                    UseAttackSkill();
                    break;
            }
        }

        private void UseHealSkill() { }

        private void UseBuildSkill() { }

        private void UseAttackSkill() { }
    }

    public class UnitBehaviour
    {
        public void UseSkill(Unit unit)
        {
            unit.UseSkill();
        }
    }
}
