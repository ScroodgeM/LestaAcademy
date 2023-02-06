//this empty line for UTF-8 BOM header

namespace LestaAcademyDemo.DesignPatterns.Behavioral.Iterator
{
    public interface IUnit
    {
        UnitType UnitType { get; }
        bool IsMovable { get; }
        string GetAlliance();
    }
}
