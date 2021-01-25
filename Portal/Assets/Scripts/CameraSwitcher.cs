using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    [SerializeField] private Camera camera1;
    [SerializeField] private Camera camera2;
    [SerializeField] private RenderTexture anotherWorldTexture;
    [SerializeField] private DrawAnotherWorld drawAnotherWorldOnCamera1;
    [SerializeField] private DrawAnotherWorld drawAnotherWorldOnCamera2;
    [SerializeField] private Transform portal1ForNearClipPlane;
    [SerializeField] private Transform portal2ForNearClipPlane;
    [SerializeField] private float defaultNearClipPlane;

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

        camera1.nearClipPlane = currentWorld == 1 ? defaultNearClipPlane : (camera1.transform.position - portal1ForNearClipPlane.position).magnitude;
        camera2.nearClipPlane = currentWorld == 2 ? defaultNearClipPlane : (camera2.transform.position - portal2ForNearClipPlane.position).magnitude;
    }

    private void SetWorld(int world)
    {
        camera1.targetTexture = world == 1 ? null : anotherWorldTexture;
        camera2.targetTexture = world == 2 ? null : anotherWorldTexture;

        drawAnotherWorldOnCamera1.enabled = world == 1;
        drawAnotherWorldOnCamera2.enabled = world == 2;

        currentWorld = world;
    }
}
