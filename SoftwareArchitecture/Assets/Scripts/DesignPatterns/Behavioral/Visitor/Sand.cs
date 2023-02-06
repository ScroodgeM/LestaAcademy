namespace LestaAcademyDemo.DesignPatterns.Behavioral.Visitor
{
    public class Sand : IVisitor
    {
        public void Visit(HumanoidUnit unit)
        {
            unit.SetSpeed(0.2f);
        }

        public void Visit(VehicleUnit unit)
        {
            unit.SetSpeed(0.1f);
        }
    }
}
