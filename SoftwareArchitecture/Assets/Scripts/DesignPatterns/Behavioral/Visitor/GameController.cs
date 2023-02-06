using System.Collections.Generic;
using UnityEngine;

namespace LestaAcademyDemo.DesignPatterns.Behavioral.Visitor
{
    public class GameController
    {
        private readonly List<Unit> allUnits = new List<Unit>();

        public void DoUpdate()
        {
            foreach (Unit unit in allUnits)
            {
                IVisitor visitor = GetVisitorFor(unit.transform.position);

                unit.Accept(visitor);
            }
        }

        private IVisitor GetVisitorFor(Vector3 position)
        {
            // analyze position and return visitor that corresponds to surface in this position

            return null;
        }
    }
}
