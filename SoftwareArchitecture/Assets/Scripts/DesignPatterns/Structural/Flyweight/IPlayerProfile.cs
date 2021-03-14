
using UnityEngine;

namespace WGADemo.DesignPatterns.Structural.Flyweight
{
    public interface IPlayerProfile
    {
        int GetPlayerId();
        string GetName();
        int GetLevel();
        int GetRating();
        Texture2D GetAvatar();
    }
}
