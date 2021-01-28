using UnityEngine;

public class CameraFollow3DLockAndMouseLook : MonoBehaviour
{
    [SerializeField] private Vector3 cameraOffset;
    [SerializeField] private Vector3 cameraRotationRelative;
    [SerializeField] private Transform target;
    [SerializeField] private float mouseSensitivity;

    private Vector2? oldMousePosition = null;

    private Vector2 userRotation = Vector2.zero;

    private void Update()
    {
        // rotate only with mouse right button pressed

        Vector2? newMousePosition = null;

        if (Input.GetMouseButton(1) == true)
        {
            newMousePosition = Input.mousePosition;
        }

        if (oldMousePosition.HasValue && newMousePosition.HasValue)
        {
            userRotation += Vector2.Scale(newMousePosition.Value - oldMousePosition.Value, new Vector2(1f / Screen.width, 1f / Screen.height)) * mouseSensitivity;
        }

        oldMousePosition = newMousePosition;
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
