using UnityEngine;

public class CameraFollowMarkers : MonoBehaviour
{
    [SerializeField] private Transform cameraPoint;
    [SerializeField] private Transform focusPoint;

    void LateUpdate()
    {
        transform.position = cameraPoint.position;
        transform.rotation = Quaternion.LookRotation(focusPoint.position - cameraPoint.position);
    }
}
