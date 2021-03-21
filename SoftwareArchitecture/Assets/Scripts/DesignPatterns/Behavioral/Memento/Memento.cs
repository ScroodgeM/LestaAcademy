
namespace WGADemo.DesignPatterns.Behavioral.Memento
{
    public class Memento
    {
        public readonly string playerMemento;
        public readonly string levelMemento;

        public Memento(string playerMemento, string levelMemento)
        {
            this.playerMemento = playerMemento;
            this.levelMemento = levelMemento;
        }
    }
}
