
namespace LestaAcademyDemo.DesignPatterns.Creational.LazyInitialization
{
    public class LazyBonus : IBonus
    {
        private IBonus realBonus = null;

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
