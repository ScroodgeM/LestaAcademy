using UnityEngine;

public class CameraFollow3DLock : MonoBehaviour
{
    [SerializeField] private Vector3 cameraOffset;
    [SerializeField] private Vector3 cameraRotationRelative;
    [SerializeField] private Transform target;

    private void LateUpdate()
    {
        transform.position = target.position + target.rotation * cameraOffset;
        transform.rotation = target.rotation * Quaternion.Euler(cameraRotationRelative);
    }
}
