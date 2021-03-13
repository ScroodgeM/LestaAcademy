
using System.Collections.Generic;

namespace WGADemo.DesignPrinciples.ProgrammingForInterfaceNotImplementation.Wrong
{
    public class GameController
    {
        private GameConfig gameConfig;
        private List<Unit> playerArmy;

        public void CreateArmy(int unitsCount)
        {
            for (int i = 0; i < unitsCount; i++)
            {
                playerArmy.Add(new Unit(gameConfig));
            }
        }

        public void MoveArmy()
        {
            foreach (Unit unit in playerArmy)
            {
                unit.Move();
            }
        }
    }

    public class GameConfig
    {
        public int unitInitialHealth = 100;
        public int unitInitialSpeed = 5;
    }

    public class Unit
    {
        private int health;
        private int speed;

        public Unit(GameConfig gameConfig)
        {
            health = gameConfig.unitInitialHealth;
            speed = gameConfig.unitInitialSpeed;
        }

        public void Move()
        {
            // move unit here
        }
    }
}
