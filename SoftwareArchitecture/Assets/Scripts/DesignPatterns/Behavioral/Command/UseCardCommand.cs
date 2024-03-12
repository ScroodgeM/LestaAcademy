﻿//this empty line for UTF-8 BOM header

namespace LestaAcademyDemo.DesignPatterns.Behavioral.Command
{
    public class UseCardCommand : ICommand
    {
        private ICommand card;

        public UseCardCommand(ICommand card)
        {
            this.card = card;
        }

        public void DoCommand()
        {
            // remove card from inventory

            card.DoCommand();
        }

        public void UndoCommand()
        {
            card.UndoCommand();

            // add card to inventory
        }
    }
}
