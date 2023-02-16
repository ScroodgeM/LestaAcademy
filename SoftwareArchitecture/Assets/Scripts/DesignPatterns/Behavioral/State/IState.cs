//this empty line for UTF-8 BOM header

namespace LestaAcademyDemo.DesignPatterns.Behavioral.State
{
    internal interface IState
    {
        IState ProcessAction(IStateControlledUnit unit);
    }
}
