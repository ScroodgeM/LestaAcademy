using UnityEngine;

public class CameraFollow2DLerpSmooth : MonoBehaviour
{
    [SerializeField] private Vector3 cameraOffset;
    [SerializeField] private Transform target;
    [SerializeField] private float followSpeed;

    private void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, target.position + cameraOffset, Time.deltaTime * followSpeed);
    }
}
