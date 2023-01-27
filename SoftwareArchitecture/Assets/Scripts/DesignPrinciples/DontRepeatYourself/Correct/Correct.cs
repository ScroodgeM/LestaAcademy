
using System.Collections.Generic;

namespace LestaAcademyDemo.DesignPrinciples.DontRepeatYourself.Correct
{
    public class Unit
    {
        public const float MaxHealth = 100f;

        private float health;

        public float GetHealth() => health;

        public void HandleHeal(float healthPoints)
        {
            health += healthPoints;
            if (health > MaxHealth) { health = MaxHealth; }
        }

        public void HandleAttack(float damagePoints)
        {
            health -= damagePoints;
        }
    }

    public abstract class UnitBehaviour
    {
        protected Unit GetLowestHealthUnit(List<Unit> units)
        {
            Unit result = null;

            foreach (Unit unit in units)
            {
                if (result == null || unit.GetHealth() < result.GetHealth())
                {
                    result = unit;
                }
            }

            return result;
        }
    }

    public class AttackUnitBehaviour : UnitBehaviour
    {
        public void UseSkill(List<Unit> allEnemies)
        {
            Unit lowestHealthUnit = GetLowestHealthUnit(allEnemies);

            Attack(lowestHealthUnit);
        }

        private void Attack(Unit unit)
        {
            unit.HandleAttack(10f);
        }
    }

    public class HealerUnitBehaviour : UnitBehaviour
    {
        public void UseSkill(List<Unit> allAllies)
        {
            Unit lowestHealthUnit = GetLowestHealthUnit(allAllies);

            if (lowestHealthUnit.GetHealth() < Unit.MaxHealth)
            {
                Heal(lowestHealthUnit);
            }
        }

        private void Heal(Unit unit)
        {
            unit.HandleHeal(10f);
        }
    }
}
