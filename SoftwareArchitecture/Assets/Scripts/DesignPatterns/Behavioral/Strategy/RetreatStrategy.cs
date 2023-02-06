using UnityEngine;

namespace LestaAcademyDemo.DesignPatterns.Behavioral.Strategy
{
    public class RetreatStrategy : IUnitStrategy
    {
        public Vector3 GetMoveGoal(UnitBehaviour unit, Vector3 enemyPosition)
        {
            Vector3 offsetToEnemy = enemyPosition - unit.GetPosition();
            return unit.GetPosition() - offsetToEnemy;
        }

        public bool ShouldAttackEnemy(UnitBehaviour unit, Vector3 enemyPosition)
        {
            return false;
        }

        public void HandleEnemyAttack(UnitBehaviour unit, Vector3 enemyPosition)
        {
            Vector3 offsetToEnemy = enemyPosition - unit.GetPosition();

            Vector3 sideStep = Quaternion.AngleAxis(90f, Vector3.up) * offsetToEnemy.normalized;

            unit.MoveToGoal(unit.GetPosition() + sideStep);
        }
    }
}
