
using TMPro;
using UnityEngine;

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
        bool keyPressed = Input.GetKey(keyToListen);
        pressedStateIndicator.SetActive(keyPressed);
    }
}
