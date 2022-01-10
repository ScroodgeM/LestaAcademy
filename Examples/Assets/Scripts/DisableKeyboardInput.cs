
using UnityEngine;
using UnityEngine.InputSystem;

public class DisableKeyboardInput : MonoBehaviour
{
    [SerializeField] private PlayerInput playerInput;

    private void Awake()
    {
        playerInput.onControlsChanged += PlayerInput_onControlsChanged;
    }

    private void PlayerInput_onControlsChanged(PlayerInput playerInput)
    {
        if (playerInput.currentControlScheme == "Gamepad")
        {
            playerInput.neverAutoSwitchControlSchemes = true;
        }
    }
}
