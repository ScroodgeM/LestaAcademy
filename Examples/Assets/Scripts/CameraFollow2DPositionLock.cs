using UnityEngine;

public class CameraFollow2DPositionLock : MonoBehaviour
{
    [SerializeField] private Vector3 cameraOffset;
    [SerializeField] private Transform target;

    private void LateUpdate()
    {
        transform.position = target.position + cameraOffset;
    }
}
