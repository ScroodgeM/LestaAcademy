using UnityEngine;

public class CameraFollow3DSmoothFollow : MonoBehaviour
{
    [SerializeField] private Vector3 cameraOffset;
    [SerializeField] private Vector3 cameraRotationRelative;
    [SerializeField] private Transform target;
    [SerializeField] private float followSpeed;

    private void FixedUpdate()
    {
        Vector3 oldPos = transform.position;
        Vector3 newPos = target.position + target.rotation * cameraOffset;
        float lerp = Time.deltaTime * followSpeed;

        transform.position = Vector3.Lerp(oldPos, newPos, lerp);
        transform.rotation =
            Quaternion.LookRotation(target.position - transform.position) *
            Quaternion.Euler(cameraRotationRelative);
    }
}
