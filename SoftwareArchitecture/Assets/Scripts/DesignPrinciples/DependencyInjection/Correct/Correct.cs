//this empty line for UTF-8 BOM header

using System;
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
        private readonly Func<IUnit> spawnUnitFunction;

        public GameController(Func<IUnit> spawnUnitFunction)
        {
            this.spawnUnitFunction = spawnUnitFunction;
        }

        public List<IUnit> SpawnArmy(int unitCount)
        {
            List<IUnit> army = new List<IUnit>();

            for (int i = 0; i < unitCount; i++)
            {
                IUnit unit = spawnUnitFunction();
                army.Add(unit);
            }

            return army;
        }
    }

    public static class DependencyContainer
    {
        public static void SpawnArmy()
        {
            GameController gameController = new GameController(CreateUnit);
            List<IUnit> army = gameController.SpawnArmy(100);
        }

        private static IUnit CreateUnit()
        {
            IGameConfig gameConfig = GameConfig.GetConfig();
            return new Unit(gameConfig);
        }
    }
}
