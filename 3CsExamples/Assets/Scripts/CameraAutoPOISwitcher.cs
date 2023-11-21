using UnityEngine;

public class CameraAutoPOISwitcher : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private LayerMask obstaclesMask;

    [Header("look down")]
    [SerializeField] private float lookDownDistanceFromPlayerToCheck;
    [SerializeField] private GameObject lookDownCameraActivator;

    [Header("look up")]
    [SerializeField] private float lookUpDistanceFromPlayerToCheck;
    [SerializeField] private GameObject lookUpCameraActivator;

    private void LateUpdate()
    {
        lookDownCameraActivator.SetActive(ShouldLookDown());
        lookUpCameraActivator.SetActive(ShouldLookUp());
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
}
