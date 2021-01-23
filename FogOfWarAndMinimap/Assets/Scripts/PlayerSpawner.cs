using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private Transform spawnRoot;
    [SerializeField] private Camera worldCamera;
    [SerializeField] private Bounds playerSpawnZone;
    [SerializeField] private GameObject spawnObject;

    private int spawnObjectsCount = 0;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) == true)
        {
            // mass spawn hack 8)
            if (Input.GetKey(KeyCode.LeftShift) == true)
            {
                Spawn(100);
            }
            else
            {
                Spawn(1);
            }
        }
    }

    private void Spawn(int count)
    {
        Ray mouseRay = worldCamera.ScreenPointToRay(Input.mousePosition);

        //assume we spawn in Y=0 surface

        Vector3 spawnPoint = mouseRay.origin - mouseRay.direction / mouseRay.direction.y * mouseRay.origin.y;

        if (playerSpawnZone.Contains(spawnPoint) == true)
        {
            for (int i = 0; i < count; i++)
            {
                Vector3 offset = Random.onUnitSphere;
                offset.y = 0f;

                GameObject instance = Instantiate(spawnObject, spawnPoint + offset, spawnObject.transform.rotation, spawnRoot);
                instance.name = $"player_{++spawnObjectsCount}";
            }
        }
    }
}
