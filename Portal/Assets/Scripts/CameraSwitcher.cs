using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    [SerializeField] private Camera camera1;
    [SerializeField] private Camera camera2;
    [SerializeField] private RenderTexture anotherWorldTexture;
    [SerializeField] private DrawAnotherWorld drawAnotherWorldOnCamera1;
    [SerializeField] private DrawAnotherWorld drawAnotherWorldOnCamera2;
    [SerializeField] private Portal portal1;
    [SerializeField] private Portal portal2;
    [SerializeField] private float defaultNearClipPlane;
    [SerializeField] private Vector3 world1to2offset;
    [SerializeField] private GameObject character;

    private int currentWorld = 0;

    private void Awake()
    {
        SetWorld(1);

        portal1.OnPlayerEnterPortal += () => { SetWorld(2); };
        portal2.OnPlayerEnterPortal += () => { SetWorld(1); };
    }

    private void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) == true && currentWorld != 1)
        {
            SetWorld(1);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) == true && currentWorld != 2)
        {
            SetWorld(2);
        }

        camera1.transform.position = transform.position - (currentWorld == 1 ? Vector3.zero : world1to2offset);
        camera2.transform.position = transform.position + (currentWorld == 2 ? Vector3.zero : world1to2offset);

        camera1.nearClipPlane = currentWorld == 1 ? defaultNearClipPlane : (camera1.transform.position - portal1.transform.position).magnitude;
        camera2.nearClipPlane = currentWorld == 2 ? defaultNearClipPlane : (camera2.transform.position - portal2.transform.position).magnitude;
    }

    private void SetWorld(int world)
    {
        camera1.targetTexture = world == 1 ? null : anotherWorldTexture;
        camera2.targetTexture = world == 2 ? null : anotherWorldTexture;

        drawAnotherWorldOnCamera1.enabled = world == 1;
        drawAnotherWorldOnCamera2.enabled = world == 2;

        if (currentWorld == 1 && world == 2)
        {
            character.SetActive(false);
            character.transform.position += world1to2offset;
            character.SetActive(true);
        }

        if (currentWorld == 2 && world == 1)
        {
            character.SetActive(false);
            character.transform.position -= world1to2offset;
            character.SetActive(true);
        }

        currentWorld = world;

        portal1.SetActive(currentWorld == 1);
        portal2.SetActive(currentWorld == 2);
    }
}
