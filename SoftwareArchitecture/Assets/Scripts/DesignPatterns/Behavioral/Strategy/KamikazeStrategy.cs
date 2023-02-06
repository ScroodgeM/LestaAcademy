//this empty line for UTF-8 BOM header
using UnityEngine;

namespace LestaAcademyDemo.DesignPatterns.Behavioral.Strategy
{
    public class KamikazeStrategy : IUnitStrategy
    {
        public Vector3 GetMoveGoal(UnitBehaviour unit, Vector3 enemyPosition)
        {
            return enemyPosition;
        }

        public bool ShouldAttackEnemy(UnitBehaviour unit, Vector3 enemyPosition)
        {
            return true;
        }

        public void HandleEnemyAttack(UnitBehaviour unit, Vector3 enemyPosition)
        {
            // do nothing
        }
    }
}
