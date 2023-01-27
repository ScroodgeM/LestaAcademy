
using System.Collections.Generic;

namespace LestaAcademyDemo.DesignPrinciples.DontRepeatYourself.Wrong
{
    public class Unit
    {
        private float health;

        public float GetHealth() => health;

        public void HandleHeal(float healthPoints)
        {
            health += healthPoints;
            if (health > 100f) { health = 100f; }
        }

        public void HandleAttack(float damagePoints)
        {
            health -= damagePoints;
        }
    }

    public class AttackUnitBehaviour
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

        private Unit GetLowestHealthUnit(List<Unit> units)
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

    public class HealerUnitBehaviour
    {
        public void UseSkill(List<Unit> allAllies)
        {
            Unit lowestHealthUnit = GetLowestHealthUnit(allAllies);

            if (lowestHealthUnit.GetHealth() < 100f)
            {
                Heal(lowestHealthUnit);
            }
        }

        private void Heal(Unit unit)
        {
            unit.HandleHeal(10f);
        }

        private Unit GetLowestHealthUnit(List<Unit> units)
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
}
