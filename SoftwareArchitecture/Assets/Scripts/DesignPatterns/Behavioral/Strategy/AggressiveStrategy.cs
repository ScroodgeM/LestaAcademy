//this empty line for UTF-8 BOM header
using UnityEngine;

namespace LestaAcademyDemo.DesignPatterns.Behavioral.Strategy
{
    public class AggressiveStrategy : IUnitStrategy
    {
        public Vector3 GetMoveGoal(UnitBehaviour unit, Vector3 enemyPosition)
        {
            Vector3 offsetToEnemy = enemyPosition - unit.GetPosition();
            offsetToEnemy = offsetToEnemy.normalized * unit.GetShortRangeWeaponRange();
            return enemyPosition - offsetToEnemy;
        }

        public bool ShouldAttackEnemy(UnitBehaviour unit, Vector3 enemyPosition)
        {
            return true; // attack even when enemy out of range
        }

        public void HandleEnemyAttack(UnitBehaviour unit, Vector3 enemyPosition)
        {
            // do nothing
        }
    }
}
