
using System;
using System.Collections.Generic;
using UnityEngine;

namespace WGADemo.DesignPatterns.Creational.Prototype
{
    public class PrototypeSelector : MonoBehaviour
    {
        [Serializable]
        private struct UnitPrototype
        {
            public UnitType unitType;
            public UnitRank unitRank;
            public GameObject unitPrefab;
        }

        [SerializeField] private UnitPrototype[] unitPrototypes;

        public IUnit CreateUnit(UnitType unitType, UnitRank unitRank)
        {
            foreach (UnitPrototype prototype in unitPrototypes)
            {
                if (prototype.unitType == unitType && prototype.unitRank == unitRank)
                {
                    GameObject prefab = prototype.unitPrefab;

                    if (prefab.GetComponent<IUnit>() != null)
                    {
                        return Instantiate(prefab).GetComponent<IUnit>();
                    }
                }
            }

            throw new KeyNotFoundException($"prototype for unit of type {unitType} and rank {unitRank} not found");
        }
    }
}
