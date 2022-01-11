
using UnityEngine;
using UnityEngine.InputSystem;

public class VehicleController : MonoBehaviour
{
    [SerializeField]
    private enum ControlMode
    {
        Local,
        World,
    }

    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float movementSpeed;
    [SerializeField] private ControlMode controlMode;

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
            switch (controlMode)
            {
                case ControlMode.Local:
                    transform.rotation *= Quaternion.AngleAxis(Time.deltaTime * rotationSpeed * moveCommand.x, Vector3.up);
                    transform.position += transform.forward * movementSpeed * moveCommand.y * Time.deltaTime;
                    break;

                case ControlMode.World:
                    Vector3 moveCommand3d = new Vector3(moveCommand.x, 0, moveCommand.y);
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(moveCommand3d), Time.deltaTime * rotationSpeed);
                    transform.position += transform.forward * Mathf.Min(moveCommand.magnitude, 1f) * movementSpeed * Time.deltaTime;
                    break;
            }
        }
    }
}
