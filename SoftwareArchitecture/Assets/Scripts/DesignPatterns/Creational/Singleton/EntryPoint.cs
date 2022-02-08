
using UnityEngine;

namespace WGADemo.DesignPatterns.Creational.Singleton
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private GameObject factoryGameObject;
        [SerializeField] private GameObject gameControllerGameObject;

        private static EntryPoint _instance;
        private static EntryPoint instance
        {
            get
            {
                if (_instance == null)
                {
                    Debug.LogError($"instance of {nameof(EntryPoint)} not exists, attach component to some scene GameObject");
                }
                return _instance;
            }
        }

        private IFactory factory;
        private IGameController gameController;

        public static IFactory Factory => instance.factory;
        public static IGameController GameController => instance.gameController;

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
            }
            else
            {
                Debug.LogError($"instance of {nameof(EntryPoint)} already exists");
            }

            factory = factoryGameObject.GetComponent<IFactory>();
            gameController = gameControllerGameObject.GetComponent<IGameController>();
        }

        public void StartGame()
        {
            EntryPoint.GameController.CreateArmy();
        }
    }
}
