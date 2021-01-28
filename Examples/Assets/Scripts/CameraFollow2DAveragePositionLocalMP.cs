using UnityEngine;

public class CameraFollow2DAveragePositionLocalMP : MonoBehaviour
{
    [SerializeField] private Vector3 cameraOffset;
    [SerializeField] private Transform[] targets;

    private void LateUpdate()
    {
        Vector3 averagePosition = Vector3.zero;
        foreach (Transform t in targets)
        {
            averagePosition += t.position;
        }
        averagePosition /= targets.Length;

        transform.position = averagePosition + cameraOffset;
    }
}
