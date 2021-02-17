using System.Collections.Generic;
using UnityEngine;

namespace Battlegrounds
{
    public class DungeonCreator : MonoBehaviour
    {
        [SerializeField] private Vector2Int size;
        [SerializeField] private int steps;
        [SerializeField] private int seed;
        [SerializeField] private float cellRealSize;
        [SerializeField] private Rect[] floorRects;
        [SerializeField] private Rect[] wallRects;
        [SerializeField] private Rect[] roofRects;

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
                        if (map[x, z] == false)
                        {
                            continue;
                        }

                        AddSurface(x, z, floorRects[Random.Range(0, floorRects.Length)], Quaternion.identity);

                        if (IsWalkable(x + 1, z) == false)
                        {
                            AddSurface(x, z, wallRects[Random.Range(0, wallRects.Length)], Quaternion.Euler(-90f, 0f, 90f));
                        }

                        if (IsWalkable(x - 1, z) == false)
                        {
                            AddSurface(x, z, wallRects[Random.Range(0, wallRects.Length)], Quaternion.Euler(-90f, 0f, -90f));
                        }

                        if (IsWalkable(x, z + 1) == false)
                        {
                            AddSurface(x, z, wallRects[Random.Range(0, wallRects.Length)], Quaternion.Euler(-90f, 0f, 0f));
                        }

                        if (IsWalkable(x, z - 1) == false)
                        {
                            AddSurface(x, z, wallRects[Random.Range(0, wallRects.Length)], Quaternion.Euler(-90f, 0f, 180f));
                        }

                        AddSurface(x, z, roofRects[Random.Range(0, roofRects.Length)], Quaternion.Euler(0f, 0f, -180f));
                    }
                }
            }

            void AddSurface(int x, int z, Rect uvRect, Quaternion rotation)
            {
                int i0 = vertices.Count;
                int i1 = i0 + 1;
                int i2 = i0 + 2;
                int i3 = i0 + 3;
                int i4 = i0 + 4;
                int i5 = i0 + 5;

                Vector3 geometricCenter = cellRealSize * (new Vector3(x + 0.5f, 0.5f, z + 0.5f) - new Vector3(size.x, 0, size.y) * 0.5f);

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
                if (x < 0 || x >= size.x || z < 0 || z >= size.y) { return false; }

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
        }
    }
}
