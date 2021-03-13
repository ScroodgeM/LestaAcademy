
using UnityEngine;

namespace WGADemo.DesignPatterns.Creational.LazyInitialization
{
    public class EntryPoint : MonoBehaviour
    {
        private static EntryPoint instance;

        public static EntryPoint Instance
        {
            get
            {
                if (instance == null)
                {
                    GameObject entryPointHolder = new GameObject();
                    DontDestroyOnLoad(entryPointHolder);

                    instance = entryPointHolder.AddComponent<EntryPoint>();
                }

                return instance;
            }
        }

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
        }

        private bool todaysWinNumberCalculated = false;
        private int todaysWinNumber;

        public int GetTodaysWinNumber()
        {
            if (todaysWinNumberCalculated == false)
            {
                // very hard calculation for sure
                todaysWinNumber = UnityEngine.Random.Range(10, 100);

                todaysWinNumberCalculated = true;
            }

            return todaysWinNumber;
        }
    }
}
