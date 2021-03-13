
using UnityEngine;

namespace WGADemo.DesignPatterns.Creational.Singleton
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private GameObject factoryGameObject;
        [SerializeField] private GameObject gameControllerGameObject;

        private static EntryPoint instance;

        public static EntryPoint Instance
        {
            get
            {
                if (instance == null)
                {
                    Debug.LogError($"instance of {nameof(EntryPoint)} not exists, attach component to some scene GameObject");
                }
                return instance;
            }
        }

        public IFactory Factory { get; private set; }
        public IGameController GameController { get; private set; }

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Debug.LogError($"instance of {nameof(EntryPoint)} already exists");
            }

            Factory = factoryGameObject.GetComponent<IFactory>();
            GameController = gameControllerGameObject.GetComponent<IGameController>();
        }

        public void StartGame()
        {
            GameController.CreateArmy();
        }
    }
}
