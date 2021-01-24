using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Transform spawnRoot;
    [SerializeField] private Bounds spawnZone;
    [SerializeField] private float spawnInterval;
    [SerializeField] private GameObject spawnObject;

    private int spawnObjectsCount = 0;

    private void Update()
    {
        if (Time.time > spawnInterval * spawnObjectsCount)
        {
            Vector3 position = new Vector3(
                Random.Range(spawnZone.min.x, spawnZone.max.x),
                Random.Range(spawnZone.min.y, spawnZone.max.y),
                Random.Range(spawnZone.min.z, spawnZone.max.z)
                );

            GameObject instance = Instantiate(spawnObject, position, spawnObject.transform.rotation, spawnRoot);
            instance.name = $"enemy_{++spawnObjectsCount}";
        }
    }
}
