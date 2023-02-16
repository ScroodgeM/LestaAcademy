//this empty line for UTF-8 BOM header

namespace LestaAcademyDemo.DesignPatterns.Behavioral.Visitor
{
    public class HumanoidUnit : Unit
    {
        public bool IsMechanoid { get; private set; }

        public override void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
