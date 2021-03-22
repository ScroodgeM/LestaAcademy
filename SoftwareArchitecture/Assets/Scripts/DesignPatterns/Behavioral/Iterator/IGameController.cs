﻿
using System.Collections.Generic;

namespace WGADemo.DesignPatterns.Behavioral.Iterator
{
    public interface IGameController
    {
        IEnumerable<IUnit> GetAllUnits();
        IEnumerable<IUnit> GetAllUnitsOfType(UnitType unitType);
        IEnumerable<IUnit> GetAllUnitsOfAlliance(string allianceName);
    }
}
