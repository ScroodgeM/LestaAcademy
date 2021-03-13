
using System.Collections.Generic;

namespace WGADemo.DesignPrinciples.ProgrammingForInterfaceNotImplementation.Correct
{
    public interface IUnit
    {
        void Move();
    }

    public interface IGameConfig
    {
        int UnitInitialHealth { get; }
        int UnitInitialSpeed { get; }
    }

    public class GameController
    {
        private List<IUnit> playerArmy;

        public void CreateArmy(int unitsCount)
        {
            for (int i = 0; i < unitsCount; i++)
            {
                playerArmy.Add(UnitFactory.CreateUnit());
            }
        }

        public void MoveArmy()
        {
            foreach (IUnit unit in playerArmy)
            {
                unit.Move();
            }
        }
    }

    public class GameConfig : IGameConfig
    {
        private readonly int unitInitialHealth = 100;
        private readonly int unitInitialSpeed = 5;

        public int UnitInitialHealth => unitInitialHealth;
        public int UnitInitialSpeed => unitInitialSpeed;
    }

    public class Unit : IUnit
    {
        private int health;
        private int speed;

        public Unit(IGameConfig gameConfig)
        {
            health = gameConfig.UnitInitialHealth;
            speed = gameConfig.UnitInitialSpeed;
        }

        public void Move()
        {
            // move unit here
        }
    }

    public static class UnitFactory
    {
        private static GameConfig gameConfig;

        public static IUnit CreateUnit()
        {
            return new Unit(gameConfig);
        }
    }
}
