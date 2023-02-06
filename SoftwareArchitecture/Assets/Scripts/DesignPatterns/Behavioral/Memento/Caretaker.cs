namespace LestaAcademyDemo.DesignPatterns.Behavioral.Memento
{
    public class Caretaker
    {
        private Originator originator;

        private Memento lastCheckpointMemento;

        private Memento newPlayerMemento; // default state for new players

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

            // load memento object from disk/cloud/server, if previously saved

            originator.RestoreFromMemento(lastCheckpointMemento);
        }

        public void OnApplicationQuit()
        {
            // save/send memento object to disk/cloud/server to restore it later
        }
    }
}
