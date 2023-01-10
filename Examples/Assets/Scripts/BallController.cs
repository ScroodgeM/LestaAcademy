
using UnityEngine;
using UnityEngine.InputSystem;

public class BallController : MonoBehaviour
{
    [SerializeField] private PlayerInput playerInput;

    [SerializeField] private CameraRotator cameraRotator;

    private void Awake()
    {
        playerInput.onActionTriggered += OnPlayerInputActionTriggered;
    }

    private void OnPlayerInputActionTriggered(InputAction.CallbackContext context)
    {
        InputAction action = context.action;

        switch (action.name)
        {
            case "Move":
                Vector2 moveCommand = action.ReadValue<Vector2>();
                HandleMoveCommand(moveCommand);
                break;

            case "Look":
                Vector2 lookCommand = action.ReadValue<Vector2>();
                HandleLookCommand(lookCommand);
                break;

            case "Jump":
                switch (action.phase)
                {
                    case InputActionPhase.Started:
                        HandleJumpCommand(true);
                        break;
                    case InputActionPhase.Canceled:
                        HandleJumpCommand(false);
                        break;
                }
                break;
        }
    }

    private void HandleMoveCommand(Vector2 moveCommand)
    {
        Vector3 position = transform.position;
        position.x = moveCommand.x;
        position.z = moveCommand.y;
        transform.position = position;
    }

    private void HandleLookCommand(Vector2 lookCommand)
    {
        cameraRotator.SetLookOffset(lookCommand);
    }

    private void HandleJumpCommand(bool jumping)
    {
        Vector3 position = transform.position;
        position.y = jumping ? 1 : 0;
        transform.position = position;
    }
}
