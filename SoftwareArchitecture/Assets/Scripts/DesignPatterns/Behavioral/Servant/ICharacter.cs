using System.Collections.Generic;

namespace LestaAcademyDemo.DesignPatterns.Behavioral.Servant
{
    public interface ICharacter
    {
        int GetBasePower();
        int GetLevel();
        int GetEnhanceMultiplier();
        IEnumerable<ICharacterItem> GetItems();
    }

    public interface ICharacterItem
    {
        int GetPower();
    }
}
