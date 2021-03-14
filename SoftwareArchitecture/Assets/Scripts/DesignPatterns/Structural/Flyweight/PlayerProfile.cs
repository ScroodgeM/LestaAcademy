
using System.Collections.Generic;
using UnityEngine;

namespace WGADemo.DesignPatterns.Structural.Flyweight
{
    public class PlayerProfile:IPlayerProfile
    {
        private readonly int playerId;
        private readonly int level;
        private readonly int rating;
        private readonly string name;
        private readonly Texture2D avatar;

        public PlayerProfile(int playerId, ref IDictionary<int, Texture2D> avatarsCache)
        {
            this.playerId = playerId;
            this.name = $"Player {playerId}";
            this.level = 1;
            this.rating = 0;

            if (avatarsCache.TryGetValue(playerId, out Texture2D avatar) == true)
            {
                this.avatar = avatar;
            }
            else
            {
                this.avatar = null; // resolve real avatar here

                // store avatar in dictionary for future use
                avatarsCache.Add(playerId, avatar);
            }
        }

        public Texture2D GetAvatar() => avatar;
        public int GetLevel() => level;
        public string GetName() => name;
        public int GetPlayerId() => playerId;
        public int GetRating() => rating;
    }
}
