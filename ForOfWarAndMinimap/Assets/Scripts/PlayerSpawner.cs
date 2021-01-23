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
            Spawn();
        }

        // mass spawn hack 8)
        if (Input.GetMouseButton(0) == true && Input.GetKey(KeyCode.LeftShift) == true)
        {
            Spawn();
        }
    }

    private void Spawn()
    {
        Ray mouseRay = worldCamera.ScreenPointToRay(Input.mousePosition);

        //assume we spawn in Y=0 surface

        Vector3 spawnPoint = mouseRay.origin - mouseRay.direction / mouseRay.direction.y * mouseRay.origin.y;

        if (playerSpawnZone.Contains(spawnPoint) == true)
        {
            GameObject instance = Instantiate(spawnObject, spawnPoint, spawnObject.transform.rotation, spawnRoot);
            instance.name = $"player_{++spawnObjectsCount}";
        }
    }
}
