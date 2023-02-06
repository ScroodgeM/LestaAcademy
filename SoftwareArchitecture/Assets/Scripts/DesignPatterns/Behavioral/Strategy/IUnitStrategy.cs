using UnityEngine;

namespace LestaAcademyDemo.DesignPatterns.Behavioral.Strategy
{
    public interface IUnitStrategy
    {
        Vector3 GetMoveGoal(UnitBehaviour unit, Vector3 enemyPosition);

        void HandleEnemyAttack(UnitBehaviour unit, Vector3 enemyPosition);

        bool ShouldAttackEnemy(UnitBehaviour unit, Vector3 enemyPosition);
    }
}
