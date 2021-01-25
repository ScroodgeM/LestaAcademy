using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField] private Transform rotatorX;
    [SerializeField] private Transform rotatorY;
    [SerializeField] private float mouseSensitivity;

    private Vector2? oldMousePosition = null;

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
            Rotate(newMousePosition.Value - oldMousePosition.Value);
        }

        oldMousePosition = newMousePosition;
    }

    private void Rotate(Vector2 delta)
    {
        rotatorX.Rotate(Vector3.left, delta.y * mouseSensitivity / Screen.height, Space.Self);
        rotatorY.Rotate(Vector3.up, delta.x * mouseSensitivity / Screen.width, Space.Self);
    }
}
