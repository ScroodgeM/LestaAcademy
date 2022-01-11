
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraFollow3DLockAndMouseLook : MonoBehaviour
{
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private Vector3 cameraOffset;
    [SerializeField] private Vector3 cameraRotationRelative;
    [SerializeField] private Transform target;
    [SerializeField] private float mouseSensitivity;
    [SerializeField] private float yAngleMin;
    [SerializeField] private float yAngleMax;

    private Vector2 userRotation = Vector2.zero;

    private void Awake()
    {
        playerInput.onActionTriggered += OnPlayerInputActionTriggered;
    }

    private void OnPlayerInputActionTriggered(InputAction.CallbackContext context)
    {
        switch (context.action.name)
        {
            case "Look":
                Vector2 delta = context.action.ReadValue<Vector2>();
                userRotation += Vector2.Scale(delta, new Vector2(1f / Screen.width, 1f / Screen.height)) * mouseSensitivity;
                userRotation.y = Mathf.Clamp(userRotation.y, yAngleMin, yAngleMax);
                break;
        }
    }

    private void LateUpdate()
    {
        Quaternion targetRotation = target.rotation * Quaternion.AngleAxis(userRotation.x, Vector3.up);
        transform.position = target.position + targetRotation * Quaternion.AngleAxis(-userRotation.y, Vector3.right) * cameraOffset;

        transform.rotation =
            Quaternion.LookRotation(target.position - transform.position) *
            Quaternion.Euler(cameraRotationRelative);
    }
}
