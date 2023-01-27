
using System.Collections.Generic;
using UnityEngine;

namespace LestaAcademyDemo.DesignPatterns.Behavioral.Servant
{
    public class PlayerProfileWindow : MonoBehaviour
    {
        private void ShowProfile(List<ICharacter> characters)
        {
            int allCharactersPower = CharacterPowerCalculator.GetPower(characters);

            // display profile and all characters summary power
        }
    }
}
