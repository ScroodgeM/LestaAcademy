using System;
using System.Collections.Generic;
using System.Threading;

namespace LestaAcademyDemo.DesignPatterns.Structural.Proxy
{
    public class ServerConnectorProxy : IServerConnector
    {
        private IServerConnector realConnector;

        private readonly Dictionary<string, LevelInfo> levelInfoCache = new Dictionary<string, LevelInfo>();

        public ServerConnectorProxy(IServerConnector realConnector)
        {
            this.realConnector = realConnector;
        }

        public LevelInfo GetLevelInfo(string levelId)
        {
            if (levelInfoCache.TryGetValue(levelId, out LevelInfo levelInfo) == true)
            {
                return levelInfo;
            }

            levelInfo = realConnector.GetLevelInfo(levelId);
            levelInfoCache.Add(levelId, levelInfo);
            return levelInfo;
        }

        public PlayerState GetPlayerState() => realConnector.GetPlayerState();

        public void SavePlayerState(PlayerState playerState, Action<bool> result)
        {
            GameSaver gameSaver = new GameSaver(realConnector, playerState);

            Thread saveGameThread = new Thread(gameSaver.Run);

            saveGameThread.Start();

            result(true);
        }

        private class GameSaver
        {
            private IServerConnector connector;
            private PlayerState playerState;
            private int attemptsLeft = 5;

            public GameSaver(IServerConnector connector, PlayerState playerState)
            {
                this.connector = connector;
                this.playerState = playerState;
            }

            public void Run()
            {
                connector.SavePlayerState(playerState, result =>
                {
                    if (result == false)
                    {
                        // try again if save game failed
                        attemptsLeft--;
                        if (attemptsLeft > 0)
                        {
                            Run();
                        }
                        else
                        {
                            // report upwards about save game fail
                        }
                    }
                });
            }
        }
    }
}
