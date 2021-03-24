
using System.Collections.Generic;
using UnityEngine;

namespace WGADemo.DesignPatterns.Behavioral.Servant
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
