﻿//this empty line for UTF-8 BOM header
using System.Collections.Generic;

namespace LestaAcademyDemo.DesignPatterns.Behavioral.Iterator
{
    public interface IGameController
    {
        IEnumerable<IUnit> GetAllUnits();
        IEnumerable<IUnit> GetAllUnitsOfType(UnitType unitType);
        IEnumerable<IUnit> GetAllUnitsOfAlliance(string allianceName);
    }
}
