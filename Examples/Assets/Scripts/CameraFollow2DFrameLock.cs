using UnityEngine;

public class CameraFollow2DFrameLock : MonoBehaviour
{
    [SerializeField] private Vector3 cameraOffset;
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 frameExtent;

    private void LateUpdate()
    {
        Vector3 oldPosition = transform.position;
        Vector3 centerPosition = target.position + cameraOffset;

        Vector3 newPosition;

        newPosition.x = Mathf.Clamp(oldPosition.x, centerPosition.x - frameExtent.x, centerPosition.x + frameExtent.x);
        newPosition.z = Mathf.Clamp(oldPosition.z, centerPosition.z - frameExtent.z, centerPosition.z + frameExtent.z);

        newPosition.y = centerPosition.y;

        transform.position = newPosition;
    }
}
