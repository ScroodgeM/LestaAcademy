//this empty line for UTF-8 BOM header

namespace LestaAcademyDemo.DesignPatterns.Behavioral.Memento
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
