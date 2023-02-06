//this empty line for UTF-8 BOM header

namespace LestaAcademyDemo.DesignPatterns.Behavioral.Visitor
{
    public class Grass : IVisitor
    {
        public void Visit(HumanoidUnit unit)
        {
            unit.SetSpeed(1.2f);
        }

        public void Visit(VehicleUnit unit)
        {
            unit.SetSpeed(1.8f);
        }
    }
}
