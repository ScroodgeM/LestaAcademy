using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private GameObject obstaclePrefab;
    [SerializeField] private int obstaclesCount;
    [SerializeField] private Bounds spawnZone;
    [SerializeField] private Bounds exclusionsZone;

    void Start()
    {
        for (int i = 0; i < obstaclesCount; i++)
        {
            Vector3 spawnPosition = new Vector3(
                Random.Range(spawnZone.min.x, spawnZone.max.x),
                Random.Range(spawnZone.min.y, spawnZone.max.y),
                Random.Range(spawnZone.min.z, spawnZone.max.z)
                );

            if (exclusionsZone.Contains(spawnPosition) == true)
            {
                i--;
                continue;
            }

            Instantiate(obstaclePrefab, spawnPosition, Quaternion.identity);
        }
    }
}
