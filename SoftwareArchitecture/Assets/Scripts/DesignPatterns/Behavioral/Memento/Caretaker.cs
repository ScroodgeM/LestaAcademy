//this empty line for UTF-8 BOM header

namespace LestaAcademyDemo.DesignPatterns.Behavioral.Memento
{
    public class Caretaker
    {
        private readonly Originator originator;

        private Memento lastCheckpointMemento;

        private Memento newPlayerMemento; // default state for new players

        public Caretaker(PlayerController playerController, LevelController levelController)
        {
            originator = new Originator(playerController, levelController);
        }

        public void OnCheckpointReached()
        {
            lastCheckpointMemento = originator.CreateMemento();
        }

        public void OnGameOver()
        {
            originator.RestoreFromMemento(lastCheckpointMemento);
        }

        public void OnApplicationStart()
        {
            lastCheckpointMemento = newPlayerMemento;

            // load/request memento object from disk/cloud/server, if previously saved

            originator.RestoreFromMemento(lastCheckpointMemento);
        }

        public void OnApplicationQuit()
        {
            Memento memento = originator.CreateMemento();

            // save/send memento object to disk/cloud/server to restore it later
        }
    }
}
