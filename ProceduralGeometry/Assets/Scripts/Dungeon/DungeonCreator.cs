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
            int vertexCount = 6 * cellsCountX * cellsCountZ;

            Vector3[] vertices = new Vector3[vertexCount];
            Vector2[] uv = new Vector2[vertexCount];
            Color[] colors = new Color[vertexCount];
            int[] triangles = new int[vertexCount];

            GenerateVertices();

            Mesh mesh = new Mesh
            {
                name = "Dungeon",
                vertices = vertices,
                uv = uv,
                colors = colors,
                triangles = triangles,
            };

            mesh.RecalculateBounds();

            GetComponent<MeshFilter>().sharedMesh = mesh;
            GetComponent<MeshCollider>().sharedMesh = mesh;

            void GenerateVertices()
            {
                for (int x = 0; x < cellsCountX; x++)
                {
                    for (int z = 0; z < cellsCountZ; z++)
                    {
                        int i0 = 6 * (x + z * cellsCountX);
                        int i1 = i0 + 1;
                        int i2 = i0 + 2;
                        int i3 = i0 + 3;
                        int i4 = i0 + 4;
                        int i5 = i0 + 5;

                        Vector2 centerOffset = Vector2.one * (-cellRealSize * 0.5f);
                        Vector2 v2_00 = new Vector2(x + 0, z + 0) * cellRealSize + centerOffset;
                        Vector2 v2_01 = new Vector2(x + 0, z + 1) * cellRealSize + centerOffset;
                        Vector2 v2_10 = new Vector2(x + 1, z + 0) * cellRealSize + centerOffset;
                        Vector2 v2_11 = new Vector2(x + 1, z + 1) * cellRealSize + centerOffset;

                        vertices[i0] = new Vector3(v2_00.x, 0f, v2_00.y);
                        vertices[i1] = new Vector3(v2_01.x, 0f, v2_01.y);
                        vertices[i2] = new Vector3(v2_11.x, 0f, v2_11.y);
                        vertices[i3] = new Vector3(v2_00.x, 0f, v2_00.y);
                        vertices[i4] = new Vector3(v2_11.x, 0f, v2_11.y);
                        vertices[i5] = new Vector3(v2_10.x, 0f, v2_10.y);

                        uv[i0] = v2_00;
                        uv[i1] = v2_01;
                        uv[i2] = v2_11;
                        uv[i3] = v2_00;
                        uv[i4] = v2_11;
                        uv[i5] = v2_10;

                        colors[i0] = map[x, z] ? Color.white : Color.black;
                        colors[i1] = map[x, z] ? Color.white : Color.black;
                        colors[i2] = map[x, z] ? Color.white : Color.black;
                        colors[i3] = map[x, z] ? Color.white : Color.black;
                        colors[i4] = map[x, z] ? Color.white : Color.black;
                        colors[i5] = map[x, z] ? Color.white : Color.black;

                        triangles[i0] = i0;
                        triangles[i1] = i1;
                        triangles[i2] = i2;
                        triangles[i3] = i3;
                        triangles[i4] = i4;
                        triangles[i5] = i5;
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
