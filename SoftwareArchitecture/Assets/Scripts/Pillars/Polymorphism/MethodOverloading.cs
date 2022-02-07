
using System;
using UnityEngine;

namespace WGADemo.Pillars.Polymorphism
{
    public interface IUnit
    {
        void Attack(Vector3 position);
        void Attack(IUnit enemyUnit);
    }

    public class UserCommandController
    {
        private IUnit selectedUnit; // here we keep currenctly selected unit

        public void Attack(Vector3 position)
        {
            IUnit enemyUnit = FindEnemyUnitAtPosition(position);

            if (enemyUnit != null)
            {
                selectedUnit.Attack(enemyUnit);
            }
            else
            {
                selectedUnit.Attack(position);
            }
        }

        private static IUnit FindEnemyUnitAtPosition(Vector3 position)
        {
            throw new NotImplementedException();
        }
    }
}
