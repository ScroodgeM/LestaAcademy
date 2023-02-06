//this empty line for UTF-8 BOM header

namespace LestaAcademyDemo.DesignPatterns.Behavioral.Command
{
    public interface ICommand
    {
        void DoCommand();
        void UndoCommand();
    }
}
