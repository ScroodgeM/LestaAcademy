
namespace LestaAcademyDemo.DesignPatterns.Behavioral.Memento
{
    public class Originator
    {
        PlayerController playerController;
        LevelController levelController;

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
