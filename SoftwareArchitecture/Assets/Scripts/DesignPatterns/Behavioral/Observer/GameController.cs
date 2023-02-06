//this empty line for UTF-8 BOM header

namespace LestaAcademyDemo.DesignPatterns.Behavioral.Observer
{
    public class GameController
    {
        private static GameController instance;

        private PlayerMoney playerMoney;

        public static ISubject<ulong> PlayerMoney
        {
            get
            {
                return instance.playerMoney;
            }
        }
    }
}
