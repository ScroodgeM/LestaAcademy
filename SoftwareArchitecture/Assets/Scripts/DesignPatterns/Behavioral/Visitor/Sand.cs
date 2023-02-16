//this empty line for UTF-8 BOM header

namespace LestaAcademyDemo.DesignPatterns.Behavioral.Visitor
{
    public class Sand : IVisitor
    {
        public void Visit(HumanoidUnit unit)
        {
            if (unit.IsMechanoid == true)
            {
                unit.SetSpeedMultiplier(0.2f);
            }
            else
            {
                unit.SetSpeedMultiplier(0.5f);
            }
        }

        public void Visit(VehicleUnit unit)
        {
            if (unit.IsFlyingUnit == false)
            {
                unit.SetSpeedMultiplier(0.1f);
            }
        }
    }
}
