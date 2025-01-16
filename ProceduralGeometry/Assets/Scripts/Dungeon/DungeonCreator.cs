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
        [SerializeField] private Rect[] wallRects;
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

            List<Vector3> vertices = new List<Vector3>();
            List<Vector2> uv = new List<Vector2>();
            List<Color> colors = new List<Color>();
            List<int> triangles = new List<int>();

            GenerateVertices();

            Mesh mesh = new Mesh
            {
                name = "Dungeon",
                vertices = vertices.ToArray(),
                uv = uv.ToArray(),
                colors = colors.ToArray(),
                triangles = triangles.ToArray(),
                hideFlags = HideFlags.DontSave,
            };

            mesh.RecalculateBounds();
            mesh.RecalculateNormals();

            GetComponent<MeshFilter>().sharedMesh = mesh;
            GetComponent<MeshCollider>().sharedMesh = mesh;

            void GenerateVertices()
            {
                for (int x = 0; x < cellsCountX; x++)
                {
                    for (int z = 0; z < cellsCountZ; z++)
                    {
                        Vector3 geometricCenter = cellRealSize * (new Vector3(x + 0.5f, 0.5f, z + 0.5f) - new Vector3(size.x, 0, size.y) * 0.5f);

                        if (map[x, z] == false)
                        {
                            continue;
                        }

                        AddSurface(geometricCenter + cellRealSize * 3 * Vector3.down, wallRects[0], Quaternion.identity);

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

            void AddSurface(Vector3 geometricCenter, Rect uvRect, Quaternion rotation)
            {
                int i0 = vertices.Count;
                int i1 = i0 + 1;
                int i2 = i0 + 2;
                int i3 = i0 + 3;
                int i4 = i0 + 4;
                int i5 = i0 + 5;

                Vector3 v3_00 = rotation * new Vector3(-0.5f, -0.5f, -0.5f) * cellRealSize + geometricCenter;
                Vector3 v3_01 = rotation * new Vector3(-0.5f, -0.5f, +0.5f) * cellRealSize + geometricCenter;
                Vector3 v3_11 = rotation * new Vector3(+0.5f, -0.5f, +0.5f) * cellRealSize + geometricCenter;
                Vector3 v3_10 = rotation * new Vector3(+0.5f, -0.5f, -0.5f) * cellRealSize + geometricCenter;

                vertices.Add(v3_00);
                vertices.Add(v3_01);
                vertices.Add(v3_11);
                vertices.Add(v3_00);
                vertices.Add(v3_11);
                vertices.Add(v3_10);

                uv.Add(uvRect.min);
                uv.Add(new Vector2(uvRect.xMin, uvRect.yMax));
                uv.Add(uvRect.max);
                uv.Add(uvRect.min);
                uv.Add(uvRect.max);
                uv.Add(new Vector2(uvRect.xMax, uvRect.yMin));

                colors.Add(Color.white);
                colors.Add(Color.white);
                colors.Add(Color.white);
                colors.Add(Color.white);
                colors.Add(Color.white);
                colors.Add(Color.white);

                triangles.Add(i0);
                triangles.Add(i1);
                triangles.Add(i2);
                triangles.Add(i3);
                triangles.Add(i4);
                triangles.Add(i5);
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
            var mesh = GetComponent<MeshFilter>().sharedMesh;

            if (mesh != null)
            {
                DestroyImmediate(mesh);
            }

            GetComponent<MeshFilter>().sharedMesh = null;
            GetComponent<MeshCollider>().sharedMesh = null;

            foreach (GameObject generatedInstance in generatedInstances)
            {
                DestroyImmediate(generatedInstance);
            }
        }
    }
}
