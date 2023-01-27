
using UnityEngine;

namespace LestaAcademyDemo.DesignPatterns.Behavioral.Command
{
    public class TakeItemCommand : ICommand
    {
        private readonly int itemId;

        private Vector3 itemPositionForUndoOperation;

        public TakeItemCommand(int itemId)
        {
            this.itemId = itemId;
        }

        public void DoCommand()
        {
            // find item by id
            // remember item's position
            // destroy item
            // add item to inventory
        }

        public void UndoCommand()
        {
            // spawn item in previously stored position
            // remove item from inventory
        }
    }
}
