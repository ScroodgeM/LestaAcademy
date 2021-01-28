using UnityEngine;

public class CameraFollow2DTargetFocusAndZoom : MonoBehaviour
{
    [SerializeField] private Vector3 cameraOffset;
    [SerializeField] private Transform player;
    [SerializeField] private Transform enemy;
    [SerializeField] private Camera orthoCamera;
    [SerializeField] private float minCameraSize;
    [SerializeField] private float maxCameraSize;

    private void LateUpdate()
    {
        Vector3 averagePosition = 0.5f * (player.position + enemy.position);
        transform.position = averagePosition + cameraOffset;

        float distanceBetween = (player.position - enemy.position).magnitude;
        orthoCamera.orthographicSize = Mathf.Clamp(distanceBetween * 0.5f, minCameraSize, maxCameraSize);
    }
}
