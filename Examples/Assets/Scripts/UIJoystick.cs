using System;
using UnityEngine;
using UnityEngine.UI;

public class UIJoystick : MonoBehaviour, IMyGameInput
{
    public Vector2 MovementCommand => currentMovementCommand;

    public event Action OnJumpCommand = () => { };

    public event Action OnFireCommand = () => { }; // not implemented yet

    public event Action OnUseSpecialSkillCommand = () => { }; // not implemented yet

    [SerializeField] private Button JumpButton;
    [SerializeField] private RectTransform uiStick;
    [SerializeField] private float uiStickMaximumOffset;

    private Vector2 currentMovementCommand;


    // Start is called before the first frame update
    private void Awake()
    {
        JumpButton.onClick.AddListener(() => { OnJumpCommand(); });
    }

    // Update is called once per frame
    private void Update()
    {
        currentMovementCommand = uiStick.localPosition / uiStickMaximumOffset;
    }
}
