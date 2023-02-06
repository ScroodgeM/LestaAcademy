﻿//this empty line for UTF-8 BOM header
using System;

namespace LestaAcademyDemo.DesignPatterns.Structural.Proxy
{
    [Serializable]
    public struct LevelInfo
    {
        public int number;
        public int floorsCount;
        public int[,] map;
    }
}
