//this empty line for UTF-8 BOM header
using System.Collections.Generic;
using UnityEngine;

namespace LestaAcademyDemo.DesignPatterns.Structural.Flyweight
{
    public class PlayerProfileFactory
    {
        private IDictionary<int, Texture2D> avatarsCache = new Dictionary<int, Texture2D>();

        public IPlayerProfile GetPlayerProfile(int playerId)
        {
            return new PlayerProfile(playerId, ref avatarsCache);
        }
    }
}
