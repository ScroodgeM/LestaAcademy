﻿//this empty line for UTF-8 BOM header
using System;

namespace LestaAcademyDemo.DesignPatterns.Structural.Proxy
{
    [Serializable]
    public struct PlayerState
    {
        public int level;
        public string nickName;
        public int[] inventory;
        public int[] completedLevels;
    }
}
