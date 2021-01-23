
using UnityEngine;

public class FogOfWarCamera : MonoBehaviour
{
    [SerializeField] private Camera fogOfWarCamera;
    [SerializeField] private Color defaultColor;

    void Start()
    {
        // required to celar render buffer with zeros
        fogOfWarCamera.clearFlags = CameraClearFlags.SolidColor;
        fogOfWarCamera.backgroundColor = defaultColor;
        fogOfWarCamera.Render();

        fogOfWarCamera.clearFlags = CameraClearFlags.Depth;
    }
}
