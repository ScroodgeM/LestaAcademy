using UnityEngine;

namespace Battlegrounds
{
    public class FlatTerrain : MonoBehaviour
    {
        [SerializeField] private Vector2 size;

        private void Awake()
        {
            GenerateTerrain();
        }

        public void GenerateTerrain()
        {
            float x = size.x * 0.5f;
            float z = size.y * 0.5f;

            Vector3[] vertices = new Vector3[4];

            vertices[0] = new Vector3(-x, 0f, -z);
            vertices[1] = new Vector3(-x, 0f, +z);
            vertices[2] = new Vector3(+x, 0f, +z);
            vertices[3] = new Vector3(+x, 0f, -z);

            int[] triangles = new int[6];

            triangles[0] = 0;
            triangles[1] = 1;
            triangles[2] = 2;

            triangles[3] = 0;
            triangles[4] = 2;
            triangles[5] = 3;

            Vector2[] uv = new Vector2[4];

            uv[0] = new Vector2(0f, 0f);
            uv[1] = new Vector2(0f, size.y);
            uv[2] = new Vector2(size.x, size.y);
            uv[3] = new Vector2(size.x, 0f);

            Mesh mesh = new Mesh
            {
                name = "FlatTerrain",
                vertices = vertices,
                triangles = triangles,
                uv = uv,
            };

            mesh.RecalculateBounds();
            mesh.RecalculateNormals();

            GetComponent<MeshFilter>().sharedMesh = mesh;
            GetComponent<MeshCollider>().sharedMesh = mesh;
        }

        public void Clear()
        {
            Mesh mesh = GetComponent<MeshFilter>().sharedMesh;
            if (mesh != null)
            {
                DestroyImmediate(mesh);
            }
        }
    }
}
