
using System.Collections.Generic;

namespace WGADemo.DesignPrinciples.DependencyInjection.Correct
{
    public interface IGameConfig
    {
        int StartUnitHealth { get; }
        int StartUnitSpeed { get; }
    }

    public class GameConfig : IGameConfig
    {
        private static GameConfig instance;

        public static IGameConfig GetConfig() => instance;

        public int StartUnitHealth => 100;

        public int StartUnitSpeed => 5;
    }

    public class Unit
    {
        private int speed;
        private int health;

        public Unit(IGameConfig gameConfig)
        {
            speed = gameConfig.StartUnitSpeed;
            health = gameConfig.StartUnitHealth;
        }
    }

    public class GameController
    {
        public List<Unit> SpawnArmy(int unitCount)
        {
            List<Unit> army = new List<Unit>();

            for (int i = 0; i < unitCount; i++)
            {
                Unit unit = DependencyContainer.CreateUnit();
                army.Add(unit);
            }

            return army;
        }
    }

    public static class DependencyContainer
    {
        public static Unit CreateUnit()
        {
            IGameConfig gameConfig = GameConfig.GetConfig();
            return new Unit(gameConfig);
        }
    }
}
