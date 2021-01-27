using UnityEngine;

public class CameraFollow2DFrameLock : MonoBehaviour
{
    [SerializeField] private Vector3 cameraOffset;
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 frameExtent;

    private void LateUpdate()
    {
        Vector3 oldPosition = transform.position;
        Vector3 newPosition = target.position + cameraOffset;

        for (int i = 0; i < 3; i++)
        {
            newPosition[i] = Mathf.MoveTowards(newPosition[i], oldPosition[i], frameExtent[i]);
        }

        transform.position = newPosition;
    }
}
