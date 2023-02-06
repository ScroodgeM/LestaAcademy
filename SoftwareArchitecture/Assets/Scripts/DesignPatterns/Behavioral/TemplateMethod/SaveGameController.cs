using System;
using System.Collections.Generic;

namespace LestaAcademyDemo.DesignPatterns.Behavioral.TemplateMethod
{
    public abstract class SaveGameController
    {
        public string GameName => gameName;
        public GameState GameState => gameState;

        private string gameName;
        private GameState gameState;

        public void LoadGame(string name)
        {
            LoadGame(name, game =>
            {
                this.gameName = name;
                this.gameState = game;
            });
        }

        public void AddMoney(ulong money)
        {
            gameState.money += money;
            SaveGame();
        }

        public void SaveGame()
        {
            SaveGame(gameName, gameState);

            GetGamesList(games =>
            {
                List<string> gamesList = new List<string>(games);
                if (gamesList.Contains(gameName) == false)
                {
                    gamesList.Add(gameName);
                    SetGamesList(gamesList);
                }
            });
        }

        public void DeleteGame()
        {
            DeleteGame(gameName);

            GetGamesList(games =>
            {
                List<string> gamesList = new List<string>(games);
                if (gamesList.Contains(gameName) == true)
                {
                    gamesList.Remove(gameName);
                    SetGamesList(gamesList);
                }
            });

            gameName = default;
            gameState = default;
        }

        protected abstract void GetGamesList(Action<IEnumerable<string>> onLoad);
        protected abstract void SetGamesList(List<string> gamesList);

        protected abstract void DeleteGame(string name);
        protected abstract void SaveGame(string name, GameState gameState);
        protected abstract void LoadGame(string name, Action<GameState> onLoad);
    }
}
