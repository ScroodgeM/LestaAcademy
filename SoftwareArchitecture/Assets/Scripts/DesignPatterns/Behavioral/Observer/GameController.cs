//this empty line for UTF-8 BOM header

namespace LestaAcademyDemo.DesignPatterns.Behavioral.Observer
{
    public class GameController
    {
        private PlayerMoney playerMoney;

        public ISubject<Money> PlayerMoney
        {
            get
            {
                return playerMoney;
            }
        }
    }
}
