//this empty line for UTF-8 BOM header
using UnityEngine;

namespace LestaAcademyDemo.DesignPatterns.Behavioral.Command
{
    public class MoveCommand : ICommand
    {
        private readonly Vector3 step;

        public MoveCommand(Vector3 step)
        {
            this.step = step;
        }

        public void DoCommand()
        {
            // move something to 'step'
        }

        public void UndoCommand()
        {
            // move something to '-step'
        }
    }
}
