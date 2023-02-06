//this empty line for UTF-8 BOM header

namespace LestaAcademyDemo.DesignPatterns.Behavioral.Observer
{
    public class PlayerMoney : SubjectBase<ulong>
    {
        public void SetNewMoney(ulong newMoney)
        {
            this.Value = newMoney;
        }
    }
}
