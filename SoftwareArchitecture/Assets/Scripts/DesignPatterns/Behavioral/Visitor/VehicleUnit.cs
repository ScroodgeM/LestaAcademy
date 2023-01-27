
namespace LestaAcademyDemo.DesignPatterns.Behavioral.Visitor
{
    public class VehicleUnit : Unit
    {
        public bool IsFlyingUnit { get; private set; }

        public override void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
