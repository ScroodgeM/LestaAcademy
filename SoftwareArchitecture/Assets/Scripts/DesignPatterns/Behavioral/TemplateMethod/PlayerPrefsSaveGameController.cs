
using System;
using System.Collections.Generic;
using UnityEngine;

namespace WGADemo.DesignPatterns.Behavioral.TemplateMethod
{
    public class PlayerPrefsSaveGameController : SaveGameController
    {
        const string gamesListKey = "games_list";

        [Serializable]
        private struct GamesList
        {
            public List<string> games;
        }

        protected override void DeleteGame(string name)
        {
            PlayerPrefs.DeleteKey(name);
        }

        protected override IEnumerable<string> GetAllSaves()
        {
            return GetGamesList();
        }

        protected override void LoadGame(string name, Action<GameState> onLoad)
        {
            string json = PlayerPrefs.GetString(name);
            GameState gameState = JsonUtility.FromJson<GameState>(json);
            onLoad(gameState);
        }

        protected override void SaveGame(string name, GameState gameState)
        {
            PlayerPrefs.SetString(name, JsonUtility.ToJson(gameState));
        }

        private List<string> GetGamesList()
        {
            string json = PlayerPrefs.GetString(gamesListKey);
            GamesList gamesList = JsonUtility.FromJson<GamesList>(json);
            return gamesList.games;
        }

        private void SaveGamesList(List<string> games)
        {
            GamesList gamesList = new GamesList() { games = games };
            PlayerPrefs.SetString(gamesListKey, JsonUtility.ToJson(gamesList));
        }
    }
}
