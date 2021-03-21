﻿
using System;
using UnityEngine;

namespace WGADemo.DesignPatterns.Behavioral.Memento
{
    public class LevelController
    {
        [Serializable]
        private struct LevelState
        {
            public uint levelId;
            public Vector2Int playerPosition;
            public bool[] completedTasks;
        }

        private LevelState levelState;

        public string GetMemento()
        {
            return JsonUtility.ToJson(levelState);
        }

        public void ApplyMemento(string memento)
        {
            levelState = JsonUtility.FromJson<LevelState>(memento);
        }
    }
}
