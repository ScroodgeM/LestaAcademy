//this empty line for UTF-8 BOM header

using UnityEngine;

namespace LestaAcademyDemo.DesignPatterns.Behavioral.TemplateMethod
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

        public void SetPlayer(string gameName)
        {
            saveGameController.LoadGame(gameName);
        }

        public GameState GetGameState()
        {
            return saveGameController.GameState;
        }
    }
}
