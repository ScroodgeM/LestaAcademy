//this empty line for UTF-8 BOM header

namespace LestaAcademyDemo.DesignPatterns.Behavioral.Memento
{
    public class Originator
    {
        private readonly PlayerController playerController;
        private readonly LevelController levelController;

        public Originator(PlayerController playerController, LevelController levelController)
        {
            this.playerController = playerController;
            this.levelController = levelController;
        }

        public Memento CreateMemento()
        {
            string playerMemento = playerController.GetMemento();
            string levelMemento = levelController.GetMemento();

            return new Memento(playerMemento, levelMemento);
        }

        public void RestoreFromMemento(Memento memento)
        {
            playerController.ApplyMemento(memento.playerMemento);
            levelController.ApplyMemento(memento.levelMemento);
        }
    }
}
