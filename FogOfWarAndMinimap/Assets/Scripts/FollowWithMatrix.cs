using UnityEngine;

public class FollowWithMatrix : MonoBehaviour
{
    [SerializeField] private Transform transformToFollow;

    [SerializeField] private Matrix4x4 transformMatrix;

    void Update()
    {
        Vector4 followPosition = transformToFollow.localPosition;

        // this allows us to set static offset in 4th matrix line
        followPosition.w = 1f;

        transform.position = transformMatrix * followPosition;
    }
}
