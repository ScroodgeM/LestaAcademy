//this empty line for UTF-8 BOM header
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LestaAcademyDemo.DesignPatterns.Behavioral.Visitor
{
    public class Lava : IVisitor
    {
        public void Visit(HumanoidUnit unit)
        {
            unit.SetSpeed(0.5f);
            unit.ApplyDamage(10);
        }

        public void Visit(VehicleUnit unit)
        {
            unit.SetSpeed(0.75f);
            if (unit.IsFlyingUnit == false)
            {
                unit.ApplyDamage(10);
            }
        }
    }
}
