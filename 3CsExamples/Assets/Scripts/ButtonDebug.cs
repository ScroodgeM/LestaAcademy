
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class ButtonDebug : MonoBehaviour
{
    [SerializeField] private KeyCode keyToListen;
    [SerializeField] private TMP_Text keyToListenLabel;
    [SerializeField] private GameObject pressedStateIndicator;

    private void Awake()
    {
        keyToListenLabel.text = keyToListen.ToString();
    }

    private void Update()
    {
        bool keyPressed = GetKeyControl(keyToListen).isPressed;
        pressedStateIndicator.SetActive(keyPressed);
    }

    private static KeyControl GetKeyControl(KeyCode keyCode)
    {
        switch (keyCode)
        {
            case KeyCode.W:
                return Keyboard.current.wKey;
            case KeyCode.A:
                return Keyboard.current.aKey;
            case KeyCode.S:
                return Keyboard.current.sKey;
            case KeyCode.D:
                return Keyboard.current.dKey;

            case KeyCode.Space:
                return Keyboard.current.spaceKey;
            case KeyCode.LeftControl:
                return Keyboard.current.leftCtrlKey;

            case KeyCode.LeftArrow:
                return Keyboard.current.leftArrowKey;
            case KeyCode.RightArrow:
                return Keyboard.current.rightArrowKey;
            case KeyCode.UpArrow:
                return Keyboard.current.upArrowKey;
            case KeyCode.DownArrow:
                return Keyboard.current.downArrowKey;

            default:
                Debug.LogError($"key {keyCode} not mapped");
                return default;
        }
    }
}
