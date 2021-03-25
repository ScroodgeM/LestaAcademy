﻿
using UnityEngine;

namespace WGADemo.DesignPatterns.Behavioral.TemplateMethod
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private bool debugSaveGame;

        private SaveGameController saveGameController;

        private void Awake()
        {
            if (debugSaveGame == true)
            {
                saveGameController = new QADebugSaveGameController();
            }
            else
            {
                saveGameController = new PlayerPrefsSaveGameController();
            }
        }

        public void SetPlayer(string name)
        {
            saveGameController.LoadGame(name);
        }

        public GameState GetGameState()
        {
            return saveGameController.GameState;
        }
    }
}
