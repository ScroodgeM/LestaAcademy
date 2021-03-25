
using System;
using System.Collections.Generic;

namespace WGADemo.DesignPatterns.Behavioral.TemplateMethod
{
    public class QADebugSaveGameController : SaveGameController
    {
        protected override void DeleteGame(string name)
        {
            // do nothing
        }

        protected override IEnumerable<string> GetAllSaves()
        {
            return new string[] { "QA Game" };
        }

        protected override void LoadGame(string name, Action<GameState> onLoad)
        {
            GameState gameState = new GameState() { level = 1000, money = 1000000000, name = "QA Debug" };
            onLoad(gameState);
        }

        protected override void SaveGame(string name, GameState gameState)
        {
            // do nothing
        }
    }
}
