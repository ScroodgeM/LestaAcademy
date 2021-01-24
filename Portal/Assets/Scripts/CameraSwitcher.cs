using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    [SerializeField] private Camera camera1;
    [SerializeField] private Camera camera2;
    [SerializeField] private LayerMask world1Layers;
    [SerializeField] private LayerMask world2Layers;
    [SerializeField] private LayerMask sharedLayers;
    [SerializeField] private RenderTexture anotherWorldTexture;
    [SerializeField] private DrawAnotherWorld drawAnotherWorldOnCamera1;
    [SerializeField] private DrawAnotherWorld drawAnotherWorldOnCamera2;

    private int currentWorld = 0;

    private void Awake()
    {
        SetWorld(1);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) == true && currentWorld != 1)
        {
            SetWorld(1);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) == true && currentWorld != 2)
        {
            SetWorld(2);
        }
    }

    private void SetWorld(int world)
    {
        camera1.targetTexture = world == 1 ? null : anotherWorldTexture;
        camera2.targetTexture = world == 2 ? null : anotherWorldTexture;

        camera1.cullingMask = world1Layers | ((world == 1) ? sharedLayers : (LayerMask)0);
        camera2.cullingMask = world2Layers | ((world == 2) ? sharedLayers : (LayerMask)0);

        drawAnotherWorldOnCamera1.enabled = world == 1;
        drawAnotherWorldOnCamera2.enabled = world == 2;

        currentWorld = world;
    }
}
