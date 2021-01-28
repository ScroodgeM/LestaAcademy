using System;
using UnityEngine;

public class MoveAndJump : MonoBehaviour, IMyGameInput
{
    public Vector2 MovementCommand => currentMovementCommand;

    public event Action OnJumpCommand = () => { };

    public event Action OnFireCommand = () => { }; // not implemented yet

    public event Action OnUseSpecialSkillCommand = () => { }; // not implemented yet

    private Vector2 currentMovementCommand;

    private void Update()
    {
        currentMovementCommand.x = Input.GetAxis("Horizontal");
        currentMovementCommand.y = Input.GetAxis("Vertical");
        if (Input.GetAxis("Jump") > 0f) { OnJumpCommand(); }
    }
}
