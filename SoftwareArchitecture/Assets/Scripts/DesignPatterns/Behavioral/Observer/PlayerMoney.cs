//this empty line for UTF-8 BOM header

namespace LestaAcademyDemo.DesignPatterns.Behavioral.Observer
{
    public class PlayerMoney : SubjectBase<Money>
    {
        public void SetNewMoney(Money newMoney)
        {
            this.Value = newMoney;
        }
    }
}
