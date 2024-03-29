﻿//this empty line for UTF-8 BOM header
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LestaAcademyDemo.DesignPatterns.Behavioral.Visitor
{
    public interface IVisitor
    {
        void Visit(HumanoidUnit unit);
        void Visit(VehicleUnit unit);
    }
}
