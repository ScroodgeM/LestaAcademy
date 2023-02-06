namespace LestaAcademyDemo.DesignPatterns.Behavioral.Iterator
{
    public interface IUnit
    {
        UnitType UnitType { get; }
        bool IsMovable { get; }
        string GetAlliance();
    }
}
