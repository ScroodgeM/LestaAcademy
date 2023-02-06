﻿//this empty line for UTF-8 BOM header
using System;

namespace LestaAcademyDemo.DesignPatterns.Structural.Proxy
{
    public interface IServerConnector
    {
        LevelInfo GetLevelInfo(string levelId);

        PlayerState GetPlayerState();

        void SavePlayerState(PlayerState playerState, Action<bool> result);
    }
}
