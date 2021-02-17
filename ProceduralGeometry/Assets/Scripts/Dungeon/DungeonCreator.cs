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

                        int i0 = vertices.Count;
                        int i1 = i0 + 1;
                        int i2 = i0 + 2;
                        int i3 = i0 + 3;
                        int i4 = i0 + 4;
                        int i5 = i0 + 5;

                        Vector2 centerOffset = (Vector2)size * (-cellRealSize * 0.5f);
                        Vector2 v2_00 = new Vector2(x + 0, z + 0) * cellRealSize + centerOffset;
                        Vector2 v2_01 = new Vector2(x + 0, z + 1) * cellRealSize + centerOffset;
                        Vector2 v2_10 = new Vector2(x + 1, z + 0) * cellRealSize + centerOffset;
                        Vector2 v2_11 = new Vector2(x + 1, z + 1) * cellRealSize + centerOffset;

                        vertices.Add(new Vector3(v2_00.x, 0f, v2_00.y));
                        vertices.Add(new Vector3(v2_01.x, 0f, v2_01.y));
                        vertices.Add(new Vector3(v2_11.x, 0f, v2_11.y));
                        vertices.Add(new Vector3(v2_00.x, 0f, v2_00.y));
                        vertices.Add(new Vector3(v2_11.x, 0f, v2_11.y));
                        vertices.Add(new Vector3(v2_10.x, 0f, v2_10.y));

                        uv.Add(Vector2.zero);
                        uv.Add(Vector2.up);
                        uv.Add(Vector2.one);
                        uv.Add(Vector2.zero);
                        uv.Add(Vector2.one);
                        uv.Add(Vector2.right);

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
                }
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
