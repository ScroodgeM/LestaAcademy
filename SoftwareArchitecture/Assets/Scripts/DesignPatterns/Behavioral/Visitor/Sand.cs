//this empty line for UTF-8 BOM header

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
            if (unit.IsFlyingUnit == false)
            {
                unit.SetSpeed(0.1f);
            }
        }
    }
}
