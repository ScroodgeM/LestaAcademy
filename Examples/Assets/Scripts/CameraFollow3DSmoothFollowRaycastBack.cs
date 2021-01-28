using UnityEngine;

public class CameraFollow3DSmoothFollowRaycastBack : MonoBehaviour
{
    [SerializeField] private Vector3 cameraOffset;
    [SerializeField] private Vector3 cameraRotationRelative;
    [SerializeField] private Transform target;
    [SerializeField] private float followSpeed;
    [SerializeField] private LayerMask obstaclesLayer;

    private void LateUpdate()
    {
        Vector3 oldPos = transform.position;
        Vector3 newPos = target.position + target.rotation * cameraOffset;
        float lerp = Time.deltaTime * followSpeed;

        transform.position = Vector3.Lerp(oldPos, newPos, lerp);

        Vector3 playerToCamera = transform.position - target.position;
        Ray playerToCameraRay = new Ray(target.position, playerToCamera);
        if (Physics.Raycast(playerToCameraRay, out RaycastHit raycastHit, playerToCamera.magnitude, obstaclesLayer) == true)
        {
            transform.position = raycastHit.point;
        }

        transform.rotation =
            Quaternion.LookRotation(target.position - transform.position) *
            Quaternion.Euler(cameraRotationRelative);
    }
}
