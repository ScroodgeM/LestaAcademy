//this empty line for UTF-8 BOM header

using System;
using System.Collections.Generic;
using UnityEngine;

namespace LestaAcademyDemo.DesignPatterns.Behavioral.TemplateMethod
{
    public class PlayerPrefsSaveGameController : SaveGameController
    {
        const string gamesListKey = "games_list";

        [Serializable]
        private struct GamesList
        {
            public List<string> games;
        }

        protected override void GetGamesList(Action<IEnumerable<string>> onLoad)
        {
            string json = PlayerPrefs.GetString(gamesListKey);
            GamesList gamesList = JsonUtility.FromJson<GamesList>(json);
            onLoad(gamesList.games);
        }

        protected override void SetGamesList(List<string> games)
        {
            GamesList gamesList = new GamesList() { games = games };
            PlayerPrefs.SetString(gamesListKey, JsonUtility.ToJson(gamesList));
        }

        protected override void DeleteGame(string name)
        {
            PlayerPrefs.DeleteKey(name);
        }

        protected override void SaveGame(string name, GameState gameState)
        {
            PlayerPrefs.SetString(name, JsonUtility.ToJson(gameState));
        }

        protected override void LoadGame(string name, Action<GameState> onLoad)
        {
            string json = PlayerPrefs.GetString(name);
            GameState gameState = JsonUtility.FromJson<GameState>(json);
            onLoad(gameState);
        }
    }
}
