
namespace WGADemo.DesignPatterns.Creational.LazyInitialization
{
    public class LazyBonus : IBonus
    {
        private Bonus realBonus = null;

        public void Show(string someMessageOnBonusBox)
        {
            if (realBonus == null)
            {
                realBonus = new Bonus();
            }

            realBonus.Show(someMessageOnBonusBox);
        }
    }
}
