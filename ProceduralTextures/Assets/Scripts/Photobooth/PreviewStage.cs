
using UnityEngine;

public class PreviewStage : MonoBehaviour
{
    [SerializeField] private Transform photoTargetRoot;
    [SerializeField] private CameraController cameraController;
    [SerializeField] private GameObject environmentPrefab;

    public Texture2D MakeIconOf(GameObject photoTarget)
    {
        GameObject environmentInstance = Instantiate(environmentPrefab, transform);

        GameObject photoTargetInstance = Instantiate(photoTarget, photoTargetRoot);
        photoTargetInstance.transform.localPosition = Vector3.zero;
        photoTargetInstance.transform.localRotation = Quaternion.identity;

        Texture2D result = cameraController.MakeIcon();

        DestroyImmediate(photoTargetInstance);

        DestroyImmediate(environmentInstance);

        return result;
    }
}
