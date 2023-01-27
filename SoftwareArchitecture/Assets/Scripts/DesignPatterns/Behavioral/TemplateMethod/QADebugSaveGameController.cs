
using System;
using System.Collections.Generic;

namespace LestaAcademyDemo.DesignPatterns.Behavioral.TemplateMethod
{
    public class QADebugSaveGameController : SaveGameController
    {
        protected override void GetGamesList(Action<IEnumerable<string>> onLoad)
        {
            onLoad(new string[] { "QA Game" });
        }

        protected override void SetGamesList(List<string> gamesList)
        {
            // do nothing
        }

        protected override void DeleteGame(string name)
        {
            // do nothing
        }

        protected override void SaveGame(string name, GameState gameState)
        {
            // do nothing
        }
        protected override void LoadGame(string name, Action<GameState> onLoad)
        {
            GameState gameState = new GameState() { level = 1000, money = 1000000000, name = "QA Debug" };
            onLoad(gameState);
        }
    }
}
