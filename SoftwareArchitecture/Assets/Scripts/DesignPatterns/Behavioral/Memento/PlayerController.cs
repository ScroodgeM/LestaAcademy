//this empty line for UTF-8 BOM header
using System;
using UnityEngine;

namespace LestaAcademyDemo.DesignPatterns.Behavioral.Memento
{
    public class PlayerController
    {
        [Serializable]
        private struct PlayerState
        {
            public string name;
            public ulong coins;
        }

        private PlayerState playerState;

        public string GetMemento()
        {
            return JsonUtility.ToJson(playerState);
        }

        public void ApplyMemento(string memento)
        {
            playerState = JsonUtility.FromJson<PlayerState>(memento);
        }
    }
}
