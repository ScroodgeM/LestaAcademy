
using UnityEngine;

namespace LestaAcademyDemo.Pillars.Abstraction
{
    public class Unit : IBattleUnit
    {
        public int UnitType { get; set; }
        public int Level { get; set; }
        public float Health { get; set; }
        public float MaxHealth { get; set; }
        public int SkinType { get; set; }
        public int BattlesCount { get; set; }
        public int TrainCost { get; set; }
        public string Faction { get; set; }
    }

    public class Turret : IBattleUnit
    {
        public int UnitType { get; set; }
        public float Health { get; set; }
        public float MaxHealth { get; set; }
        public Vector3 Position { get; set; }
        public float AggroRadius { get; set; }
        public string Faction { get; set; }
    }

    public interface IBattleUnit
    {
        int UnitType { get; }
        float Health { get;  }
        float MaxHealth { get;  }
        string Faction { get; }
    }
}
