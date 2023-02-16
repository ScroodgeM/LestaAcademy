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
            if (unit.IsMechanoid == false)
            {
                unit.SetSpeedMultiplier(0.5f);
                unit.ApplyDamage(10);
            }
        }

        public void Visit(VehicleUnit unit)
        {
            if (unit.IsFlyingUnit == false)
            {
                unit.SetSpeedMultiplier(0.75f);
                unit.ApplyDamage(10);
            }
        }
    }
}
