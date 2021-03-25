
using System;
using System.Collections.Generic;
using UnityEngine;

namespace WGADemo.DesignPatterns.Behavioral.TemplateMethod
{
    public abstract class SaveGameController
    {
        public string GameName => gameName;
        public GameState GameState => gameState;

        private string gameName;
        private GameState gameState;

        public IEnumerable<string> GetGames() => GetAllSaves();

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
        }

        public void DeleteGame()
        {
            DeleteGame(gameName);
        }

        protected abstract IEnumerable<string> GetAllSaves();
        protected abstract void DeleteGame(string name);
        protected abstract void SaveGame(string name, GameState gameState);
        protected abstract void LoadGame(string name, Action<GameState> onLoad);
    }
}
