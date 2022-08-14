
namespace WGADemo.Pillars.Abstraction
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
    }

    public interface IBattleUnit
    {
        int UnitType { get; }
        int Level { get; }
        float Health { get;  }
        float MaxHealth { get;  }
    }
}
