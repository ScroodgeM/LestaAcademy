
using UnityEngine;

public class CameraRotator : MonoBehaviour
{
    [SerializeField] private Transform rotatePivot;
    [SerializeField] private Vector3 defaultRotation;
    [SerializeField] private float maxAngle;
    [SerializeField] private float rotateSensitivity;

    private Vector2 aggregatedRotateCommand = Vector2.zero;

    private void Awake()
    {
        SetLookOffset(Vector2.zero);
    }

    public void SetLookOffset(Vector2 offset)
    {
        aggregatedRotateCommand = offset * maxAngle;
        ApplyRotation();
    }

    internal void Rotate(Vector2 rotateCommand)
    {
        aggregatedRotateCommand = Vector2.ClampMagnitude(aggregatedRotateCommand + rotateCommand * rotateSensitivity, maxAngle);
        ApplyRotation();
    }

    private void ApplyRotation()
    {
        Vector3 eulerAngles = new Vector3(-aggregatedRotateCommand.y, aggregatedRotateCommand.x, 0f);
        rotatePivot.rotation = Quaternion.Euler(eulerAngles) * Quaternion.Euler(defaultRotation);
    }
}
