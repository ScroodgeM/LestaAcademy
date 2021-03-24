
using System.Collections.Generic;
using UnityEngine;

namespace WGADemo.DesignPatterns.Behavioral.Servant
{
    public class CharacterSelectWindow : MonoBehaviour
    {
        private void ShowCharacters(List<ICharacter> characters)
        {
            CharacterPowerCalculator.SortCharacters(characters);

            foreach (ICharacter character in characters)
            {
                ShowCharacter(character);
            }
        }

        private void ShowCharacter(ICharacter character)
        {
            int characterPower = CharacterPowerCalculator.GetPower(character);

            // display character
        }
    }
}
