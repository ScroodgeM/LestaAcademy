
using UnityEngine;
using UnityEngine.InputSystem;

public class BallController : MonoBehaviour
{
    [SerializeField] private PlayerInput playerInput;

    private void Awake()
    {
        playerInput.onActionTriggered += OnPlayerInputActionTriggered;
    }

    private void OnPlayerInputActionTriggered(InputAction.CallbackContext context)
    {
        switch (context.action.name)
        {
            case "Move":
                Vector2 moveCommand = context.action.ReadValue<Vector2>();
                HandleMoveCommand(moveCommand);
                break;

            case "Jump":
                switch (context.action.phase)
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

    private void HandleJumpCommand(bool jumping)
    {
        Vector3 position = transform.position;
        position.y = jumping ? 1 : 0;
        transform.position = position;
    }
}
