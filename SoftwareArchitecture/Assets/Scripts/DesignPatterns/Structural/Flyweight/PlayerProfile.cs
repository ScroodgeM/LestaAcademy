
using System.Collections.Generic;
using UnityEngine;

namespace LestaAcademyDemo.DesignPatterns.Structural.Flyweight
{
    public class PlayerProfile : IPlayerProfile
    {
        public Texture2D Avatar => avatar;
        public int Level => level;
        public string Name => name;
        public int PlayerId => playerId;
        public int Rating => rating;

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

            int avatarId = playerId % 10;

            if (avatarsCache.TryGetValue(avatarId, out Texture2D avatar) == true)
            {
                this.avatar = avatar;
            }
            else
            {
                this.avatar = null; // resolve real avatar here

                // store avatar in dictionary for future use
                avatarsCache.Add(avatarId, this.avatar);
            }
        }
    }
}
