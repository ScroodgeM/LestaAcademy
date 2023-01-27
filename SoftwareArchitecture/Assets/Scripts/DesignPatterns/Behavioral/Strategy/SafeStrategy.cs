
using UnityEngine;

namespace LestaAcademyDemo.DesignPatterns.Behavioral.Strategy
{
    public class SafeStrategy : IUnitStrategy
    {
        public Vector3 GetMoveGoal(UnitBehaviour unit, Vector3 enemyPosition)
        {
            Vector3 offsetToEnemy = enemyPosition - unit.GetPosition();
            offsetToEnemy = offsetToEnemy.normalized * unit.GetLongRangeWeaponRange();
            return enemyPosition - offsetToEnemy;
        }

        public bool ShouldAttackEnemy(UnitBehaviour unit, Vector3 enemyPosition)
        {
            Vector3 offsetToEnemy = enemyPosition - unit.GetPosition();

            float weaponRange = unit.GetLongRangeWeaponRange();

            return offsetToEnemy.sqrMagnitude <= weaponRange * weaponRange;
        }

        public void HandleEnemyAttack(UnitBehaviour unit, Vector3 enemyPosition)
        {
            Vector3 offsetToEnemy = enemyPosition - unit.GetPosition();

            Vector3 sideStep = Quaternion.AngleAxis(90f, Vector3.up) * offsetToEnemy.normalized;

            unit.MoveToGoal(unit.GetPosition() + sideStep);
        }
    }
}
