
using UnityEngine;

namespace WGADemo.DesignPatterns.Structural.Adapter.Units
{
    public interface IPlayerControllable
    {
        void MoveCommand(Vector2 direction);
        void FireCommand();
        void JumpCommand();
    }
}
