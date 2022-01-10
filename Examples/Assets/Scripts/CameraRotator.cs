
using UnityEngine;

public class CameraRotator : MonoBehaviour
{
    [SerializeField] private Transform rotatePivot;
    [SerializeField] private Vector3 defaultRotation;
    [SerializeField] private float maxAngle;

    private void Awake()
    {
        SetLookOffset(Vector2.zero);
    }

    public void SetLookOffset(Vector2 offset)
    {
        Vector3 offsetRotation = new Vector3(-offset.y, 0f, offset.x) * maxAngle;

        rotatePivot.rotation = Quaternion.Euler(offsetRotation) * Quaternion.Euler(defaultRotation);
    }
}
