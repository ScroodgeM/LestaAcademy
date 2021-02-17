using UnityEngine;

namespace Battlegrounds
{
    public class LowPolyWater : MonoBehaviour
    {
        [SerializeField] private float cellSize = 1f;
        [SerializeField] private Vector2 waterSize;

        private void Awake()
        {
            Clear();

            GenerateWater();
        }

        public void GenerateWater()
        {
            int cellsCountX = Mathf.FloorToInt(waterSize.x / cellSize);
            int cellsCountZ = Mathf.FloorToInt(waterSize.y / cellSize);
            int vertexCount = 6 * cellsCountX * cellsCountZ;

            Vector3[] vertices = new Vector3[vertexCount];
            int[] triangles = new int[vertexCount];
            Vector2[] uv1 = new Vector2[vertexCount];
            Vector2[] uv2 = new Vector2[vertexCount];

            GenerateVertices();

            Mesh mesh = new Mesh
            {
                name = "Water",
                vertices = vertices,
                triangles = triangles,
                uv = uv1,
                uv2 = uv2,
                hideFlags = HideFlags.DontSave,
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

                        Vector2 v2_00 = new Vector2(x + 0, z + 0) * cellSize - waterSize * 0.5f;
                        Vector2 v2_01 = new Vector2(x + 0, z + 1) * cellSize - waterSize * 0.5f;
                        Vector2 v2_10 = new Vector2(x + 1, z + 0) * cellSize - waterSize * 0.5f;
                        Vector2 v2_11 = new Vector2(x + 1, z + 1) * cellSize - waterSize * 0.5f;

                        vertices[i0] = new Vector3(v2_00.x, 0f, v2_00.y);
                        vertices[i1] = new Vector3(v2_01.x, 0f, v2_01.y);
                        vertices[i2] = new Vector3(v2_11.x, 0f, v2_11.y);
                        vertices[i3] = new Vector3(v2_00.x, 0f, v2_00.y);
                        vertices[i4] = new Vector3(v2_11.x, 0f, v2_11.y);
                        vertices[i5] = new Vector3(v2_10.x, 0f, v2_10.y);

                        uv1[i0] = v2_01;
                        uv2[i0] = v2_11;
                        uv1[i1] = v2_11;
                        uv2[i1] = v2_00;
                        uv1[i2] = v2_00;
                        uv2[i2] = v2_01;

                        uv1[i3] = v2_11;
                        uv2[i3] = v2_10;
                        uv1[i4] = v2_10;
                        uv2[i4] = v2_00;
                        uv1[i5] = v2_00;
                        uv2[i5] = v2_11;

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
