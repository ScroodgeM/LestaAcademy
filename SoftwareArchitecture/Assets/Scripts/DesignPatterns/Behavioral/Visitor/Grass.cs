//this empty line for UTF-8 BOM header

namespace LestaAcademyDemo.DesignPatterns.Behavioral.Visitor
{
    public class Grass : IVisitor
    {
        public void Visit(HumanoidUnit unit)
        {
            if (unit.IsMechanoid == false)
            {
                unit.SetSpeedMultiplier(1.2f);
            }
        }

        public void Visit(VehicleUnit unit)
        {
            if (unit.IsFlyingUnit == false)
            {
                unit.SetSpeedMultiplier(1.8f);
            }
        }
    }
}
