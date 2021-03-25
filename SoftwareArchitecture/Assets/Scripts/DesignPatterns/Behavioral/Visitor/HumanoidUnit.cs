
namespace WGADemo.DesignPatterns.Behavioral.Visitor
{
    public class HumanoidUnit : Unit
    {
        public override void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
