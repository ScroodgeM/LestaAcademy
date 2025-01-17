
using UnityEngine;
using UnityEngine.InputSystem;

public class FarmerController : MonoBehaviour
{
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private Animator characterAnimator;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float movementSpeed;

    private Vector2 moveCommand = Vector2.zero;

    private void Awake()
    {
        playerInput.onActionTriggered += OnPlayerInputActionTriggered;
    }

    private void OnPlayerInputActionTriggered(InputAction.CallbackContext context)
    {
        switch (context.action.name)
        {
            case "Move":
                moveCommand = context.action.ReadValue<Vector2>();
                break;
        }
    }

    private void Update()
    {
        if (moveCommand != Vector2.zero)
        {
            transform.rotation *= Quaternion.AngleAxis(Time.deltaTime * rotationSpeed * moveCommand.x, Vector3.up);
            transform.position += transform.forward * movementSpeed * moveCommand.y * Time.deltaTime;
        }

        characterAnimator.SetFloat("Speed_f", moveCommand.y);
    }
}
