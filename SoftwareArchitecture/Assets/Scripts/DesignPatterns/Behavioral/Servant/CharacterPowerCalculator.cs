
using System.Collections.Generic;

namespace LestaAcademyDemo.DesignPatterns.Behavioral.Servant
{
    public static class CharacterPowerCalculator
    {
        public static int GetPower(List<ICharacter> characters)
        {
            int result = 0;

            foreach (ICharacter character in characters)
            {
                result += GetPower(character);
            }

            return result;
        }

        public static int GetPower(ICharacter character)
        {
            int result = character.GetBasePower() * character.GetEnhanceMultiplier();

            result = result * (100 + character.GetLevel()) / 100;

            foreach (ICharacterItem item in character.GetItems())
            {
                result += item.GetPower();
            }

            return result;
        }

        private static Dictionary<ICharacter, int> powerCache = new Dictionary<ICharacter, int>();

        public static void SortCharacters(List<ICharacter> characters)
        {
            foreach (ICharacter character in characters)
            {
                powerCache[character] = GetPower(character);
            }

            characters.Sort(Sorter);

            powerCache.Clear();

            int Sorter(ICharacter a, ICharacter b)
            {
                return -powerCache[a].CompareTo(powerCache[b]);
            }
        }
    }
}
