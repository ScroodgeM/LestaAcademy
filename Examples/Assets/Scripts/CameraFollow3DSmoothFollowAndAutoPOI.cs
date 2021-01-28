using UnityEngine;

public class CameraFollow3DSmoothFollowAndAutoPOI : MonoBehaviour
{
    [SerializeField] private Vector3 cameraOffset;
    [SerializeField] private Vector3 cameraRotationRelative;
    [SerializeField] private Transform target;
    [SerializeField] private float followSpeed;
    [SerializeField] private LayerMask obstaclesMask;

    [Header("look down")]
    [SerializeField] private float lookDownDistanceFromPlayerToCheck;
    [SerializeField] private Vector3 lookDownCameraOffset;
    [SerializeField] private Vector3 lookDownCameraRotationRelative;

    [Header("look up")]
    [SerializeField] private float lookUpDistanceFromPlayerToCheck;
    [SerializeField] private Vector3 lookUpCameraOffset;
    [SerializeField] private Vector3 lookUpCameraRotationRelative;

    private void LateUpdate()
    {
        if (ShouldLookDown() == true)
        {
            SetCamera(lookDownCameraOffset, lookDownCameraRotationRelative);
        }
        else if (ShouldLookUp() == true)
        {
            SetCamera(lookUpCameraOffset, lookUpCameraRotationRelative);
        }
        else
        {
            SetCamera(cameraOffset, cameraRotationRelative);
        }
    }

    private bool ShouldLookDown()
    {
        Vector3 origin = target.position + target.forward * lookDownDistanceFromPlayerToCheck + Vector3.up;

        return Physics.Raycast(origin, Vector3.down, 2f, obstaclesMask) == false;
    }

    private bool ShouldLookUp()
    {
        return Physics.Raycast(target.position + Vector3.up, target.forward, lookUpDistanceFromPlayerToCheck, obstaclesMask);
    }

    private void SetCamera(Vector3 camOffset, Vector3 camRotationRelative)
    {
        Vector3 oldPos = transform.position;
        Vector3 newPos = target.position + target.rotation * camOffset;
        float lerp = Time.deltaTime * followSpeed;

        transform.position = Vector3.Lerp(oldPos, newPos, lerp);
        transform.rotation =
            Quaternion.LookRotation(target.position - transform.position) *
            Quaternion.Euler(camRotationRelative);
    }
}
