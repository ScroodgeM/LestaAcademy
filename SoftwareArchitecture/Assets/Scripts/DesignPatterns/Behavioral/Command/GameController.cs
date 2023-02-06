using System.Collections.Generic;

namespace LestaAcademyDemo.DesignPatterns.Behavioral.Command
{
    public class GameController
    {
        private readonly Stack<ICommand> commandsStack = new Stack<ICommand>();

        public void MakeTurn(ICommand command)
        {
            commandsStack.Push(command);

            command.DoCommand();
        }

        public void UndoTurn()
        {
            if (commandsStack.Count > 0)
            {
                ICommand cancelledCommand = commandsStack.Pop();

                cancelledCommand.UndoCommand();
            }
        }
    }
}
