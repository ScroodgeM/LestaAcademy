﻿
using UnityEngine;

namespace LestaAcademyDemo.DesignPatterns.Behavioral.Strategy
{
    public class UnitBehaviour : MonoBehaviour
    {
        private IUnitStrategy currentStrategy;

        public void SetStrategy(IUnitStrategy strategy)
        {
            this.currentStrategy = strategy;
        }

        public void ProcessUpdate(Vector3 enemyPosition)
        {
            bool shouldAttackEnemy = currentStrategy.ShouldAttackEnemy(this, enemyPosition);

            if (shouldAttackEnemy == true)
            {
                Attack(enemyPosition);
            }

            if (shouldAttackEnemy == false /* or can attack and move simulatenously */)
            {
                Vector3 moveGoal = currentStrategy.GetMoveGoal(this, enemyPosition);
                MoveToGoal(moveGoal);
            }
        }

        public Vector3 GetPosition() => transform.position;

        public float GetLongRangeWeaponRange() => 10f; // replace it with number from config

        public float GetShortRangeWeaponRange() => 2f;// replace it with number from config

        public void NotifyAboutEnemyAttack(Vector3 enemyPosition)
        {
            currentStrategy.HandleEnemyAttack(this, enemyPosition);
        }

        private void Attack(Vector3 enemyPosition)
        {
        }

        internal void MoveToGoal(Vector3 goal)
        {
        }
    }
}
