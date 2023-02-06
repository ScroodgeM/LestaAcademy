//this empty line for UTF-8 BOM header
using UnityEngine;

namespace LestaAcademyDemo.DesignPatterns.Structural.Adapter.Units
{
    public interface IPlayerControllable
    {
        void MoveCommand(Vector2 direction);
        void FireCommand();
        void JumpCommand();
    }
}
