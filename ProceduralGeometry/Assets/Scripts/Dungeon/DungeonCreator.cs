using System.Collections.Generic;
using UnityEngine;

namespace Battlegrounds
{
    public class DungeonCreator : MonoBehaviour
    {
        [System.Serializable]
        private struct GeometryTilePrefab
        {
            public Transform tilePrefab;
            public float scaleMultiplier;
        }

        [SerializeField] private Vector2Int size;
        [SerializeField] private int steps;
        [SerializeField] private int seed;
        [SerializeField] private float cellRealSize;
        [SerializeField] private GeometryTilePrefab[] floorTilePrefabs;
        [SerializeField] private GeometryTilePrefab[] wallTilePrefabs;
        [SerializeField] private GeometryTilePrefab[] roofTilePrefabs;

        private readonly List<GameObject> generatedInstances = new List<GameObject>();

        private void Awake()
        {
            Clear();

            GenerateDungeon();
        }

        public void GenerateDungeon()
        {
            bool[,] map = MapGenerator.Create2DMap(size, steps, seed);

            int cellsCountX = size.x;
            int cellsCountZ = size.y;

            for (int x = 0; x < cellsCountX; x++)
            {
                for (int z = 0; z < cellsCountZ; z++)
                {
                    if (IsWalkable(x, z) == false)
                    {
                        continue;
                    }

                    Vector3 geometricCenter = cellRealSize * (new Vector3(x + 0.5f, 0.5f, z + 0.5f) - new Vector3(size.x, 0, size.y) * 0.5f);

                    if (IsWalkable(x + 1, z) == false)
                    {
                        CreateTile(geometricCenter + cellRealSize * 0.5f * Vector3.right, wallTilePrefabs, Quaternion.AngleAxis(90, Vector3.forward));
                    }

                    if (IsWalkable(x - 1, z) == false)
                    {
                        CreateTile(geometricCenter + cellRealSize * 0.5f * Vector3.left, wallTilePrefabs, Quaternion.AngleAxis(90, Vector3.back));
                    }

                    if (IsWalkable(x, z + 1) == false)
                    {
                        CreateTile(geometricCenter + cellRealSize * 0.5f * Vector3.forward, wallTilePrefabs, Quaternion.AngleAxis(90, Vector3.left));
                    }

                    if (IsWalkable(x, z - 1) == false)
                    {
                        CreateTile(geometricCenter + cellRealSize * 0.5f * Vector3.back, wallTilePrefabs, Quaternion.AngleAxis(90, Vector3.right));
                    }

                    CreateTile(geometricCenter + cellRealSize * 0.5f * Vector3.down, floorTilePrefabs, Quaternion.identity);

                    CreateTile(geometricCenter + cellRealSize * 0.5f * Vector3.up, roofTilePrefabs, Quaternion.AngleAxis(180, Vector3.right));
                }
            }

            void CreateTile(Vector3 position, GeometryTilePrefab[] tilesBank, Quaternion baseRotation)
            {
                GeometryTilePrefab tilePrefab = tilesBank[Random.Range(0, tilesBank.Length)];
                Transform tileInstance = Instantiate(tilePrefab.tilePrefab, transform);
                tileInstance.position = position;
                tileInstance.rotation = baseRotation * Quaternion.AngleAxis(90 * Random.Range(0, 4), Vector3.up);
                tileInstance.localScale = cellRealSize * tilePrefab.scaleMultiplier * Vector3.one;
                tileInstance.gameObject.hideFlags = HideFlags.DontSave;
                generatedInstances.Add(tileInstance.gameObject);
            }

            bool IsWalkable(int x, int z)
            {
                if (x < 0 || x >= size.x || z < 0 || z >= size.y)
                {
                    return false;
                }

                return map[x, z];
            }
        }

        public void Clear()
        {
            foreach (GameObject generatedInstance in generatedInstances)
            {
                DestroyImmediate(generatedInstance);
            }
        }
    }
}
