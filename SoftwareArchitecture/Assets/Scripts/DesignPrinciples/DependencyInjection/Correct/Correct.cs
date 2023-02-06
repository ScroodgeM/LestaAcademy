//this empty line for UTF-8 BOM header
using System.Collections.Generic;

namespace LestaAcademyDemo.DesignPrinciples.DependencyInjection.Correct
{
    public interface IGameConfig
    {
        int StartUnitHealth { get; }
        int StartUnitSpeed { get; }
    }

    public interface IUnit
    {
    }

    public class GameConfig : IGameConfig
    {
        private static GameConfig instance;

        public static IGameConfig GetConfig() => instance;

        public int StartUnitHealth => 100;

        public int StartUnitSpeed => 5;
    }

    public class Unit : IUnit
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
        public List<IUnit> SpawnArmy(int unitCount)
        {
            List<IUnit> army = new List<IUnit>();

            for (int i = 0; i < unitCount; i++)
            {
                IUnit unit = DependencyContainer.CreateUnit();
                army.Add(unit);
            }

            return army;
        }
    }

    public static class DependencyContainer
    {
        public static IUnit CreateUnit()
        {
            IGameConfig gameConfig = GameConfig.GetConfig();
            return new Unit(gameConfig);
        }
    }
}
