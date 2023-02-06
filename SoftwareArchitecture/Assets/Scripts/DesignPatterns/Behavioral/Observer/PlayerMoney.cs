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
