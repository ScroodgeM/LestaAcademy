using UnityEngine;

namespace LestaAcademyDemo.DesignPatterns.Structural.Flyweight
{
    public interface IPlayerProfile
    {
        int PlayerId { get; }
        string Name { get; }
        int Level { get; }
        int Rating { get; }
        Texture2D Avatar { get; }
    }
}
