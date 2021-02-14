using System;
using UnityEngine;
using UnityEngine.Assertions;

namespace Battlegrounds
{
    [Serializable]
    public class LowPolyTerrainChunk
    {
        public GameObject gameObject;

        private readonly MeshCollider meshCollider;
        private readonly Vector3 terrainScale;

        private readonly Mesh mesh;
        private readonly Vector3[] vertices;
        private readonly Vector3[] normals;
        private readonly int[] triangles;
        private readonly Color[] colors;

        internal int mapIndexXFrom { get; private set; }
        internal int mapIndexXTo { get; private set; }
        internal int mapIndexZFrom { get; private set; }
        internal int mapIndexZTo { get; private set; }

        private bool trianglesApplied = false;

        private int cellsCountX { get { return (mapIndexXTo - mapIndexXFrom); } }
        private int cellsCountZ { get { return (mapIndexZTo - mapIndexZFrom); } }

        public LowPolyTerrainChunk(
            GameObject gameObject,
            MeshFilter meshFilter,
            MeshCollider meshCollider,
            Vector3 terrainScale,
            int chunkIndexX,
            int chunkIndexZ,
            int chunkSize,
            int totalCellsX,
            int totalCellsZ)
        {
            this.gameObject = gameObject;
            this.meshCollider = meshCollider;
            this.terrainScale = terrainScale;

            mesh = new Mesh { name = string.Format("Chunk x{0} z{1}", chunkIndexX, chunkIndexZ) };
            meshFilter.mesh = mesh;
            meshCollider.sharedMesh = mesh;

            this.mapIndexXFrom = chunkIndexX * chunkSize;
            this.mapIndexXTo = Mathf.Min((chunkIndexX + 1) * chunkSize, totalCellsX);
            this.mapIndexZFrom = chunkIndexZ * chunkSize;
            this.mapIndexZTo = Mathf.Min((chunkIndexZ + 1) * chunkSize, totalCellsZ);

            Assert.IsTrue(cellsCountX > 0);
            Assert.IsTrue(cellsCountZ > 0);

            int vertexCount = 6 * cellsCountX * cellsCountZ;
            vertices = new Vector3[vertexCount];
            normals = new Vector3[vertexCount];
            triangles = new int[vertexCount];
            colors = new Color[vertexCount];
        }

        internal bool IsInRange(int xMin, int xMax, int zMin, int zMax)
        {
            return
                xMin <= mapIndexXTo
                &&
                xMax >= mapIndexXFrom
                &&
                zMin <= mapIndexZTo
                &&
                zMax >= mapIndexZFrom;
        }

        internal void ApplyGeometry(float[,] heights)
        {
            for (int x = mapIndexXFrom; x < mapIndexXTo; x++)
            {
                for (int z = mapIndexZFrom; z < mapIndexZTo; z++)
                {
                    int localX = x - mapIndexXFrom;
                    int localZ = z - mapIndexZFrom;

                    int meshVertexIndex0 = 6 * (localX + localZ * cellsCountX);
                    int meshVertexIndex1 = meshVertexIndex0 + 1;
                    int meshVertexIndex2 = meshVertexIndex0 + 2;
                    int meshVertexIndex3 = meshVertexIndex0 + 3;
                    int meshVertexIndex4 = meshVertexIndex0 + 4;
                    int meshVertexIndex5 = meshVertexIndex0 + 5;

                    Vector3 vertex00 = new Vector3((localX + 0) * terrainScale.x, heights[x + 0, z + 0], (localZ + 0) * terrainScale.z);
                    Vector3 vertex01 = new Vector3((localX + 0) * terrainScale.x, heights[x + 0, z + 1], (localZ + 1) * terrainScale.z);
                    Vector3 vertex10 = new Vector3((localX + 1) * terrainScale.x, heights[x + 1, z + 0], (localZ + 0) * terrainScale.z);
                    Vector3 vertex11 = new Vector3((localX + 1) * terrainScale.x, heights[x + 1, z + 1], (localZ + 1) * terrainScale.z);

                    vertices[meshVertexIndex0] = vertex00;
                    vertices[meshVertexIndex1] = vertex01;
                    vertices[meshVertexIndex2] = vertex11;
                    vertices[meshVertexIndex3] = vertex00;
                    vertices[meshVertexIndex4] = vertex11;
                    vertices[meshVertexIndex5] = vertex10;

                    Vector3 normal000111 = Vector3.Cross(vertex01 - vertex00, vertex11 - vertex00).normalized;
                    Vector3 normal001011 = Vector3.Cross(vertex11 - vertex00, vertex10 - vertex00).normalized;

                    normals[meshVertexIndex0] = normal000111;
                    normals[meshVertexIndex1] = normal000111;
                    normals[meshVertexIndex2] = normal000111;
                    normals[meshVertexIndex3] = normal001011;
                    normals[meshVertexIndex4] = normal001011;
                    normals[meshVertexIndex5] = normal001011;

                    if (!trianglesApplied)
                    {
                        triangles[meshVertexIndex0] = meshVertexIndex0;
                        triangles[meshVertexIndex1] = meshVertexIndex1;
                        triangles[meshVertexIndex2] = meshVertexIndex2;
                        triangles[meshVertexIndex3] = meshVertexIndex3;
                        triangles[meshVertexIndex4] = meshVertexIndex4;
                        triangles[meshVertexIndex5] = meshVertexIndex5;
                    }
                }
            }
            mesh.vertices = vertices;
            mesh.normals = normals;
            if (!trianglesApplied)
            {
                mesh.triangles = triangles;
            }
            mesh.RecalculateBounds();
            meshCollider.sharedMesh = mesh;
            trianglesApplied = true;
        }

        internal void ApplyColors(Color[,] colorMap)
        {
            for (int x = mapIndexXFrom; x < mapIndexXTo; x++)
            {
                for (int z = mapIndexZFrom; z < mapIndexZTo; z++)
                {
                    int localX = x - mapIndexXFrom;
                    int localZ = z - mapIndexZFrom;

                    int meshVertexIndex0 = 6 * (localX + localZ * cellsCountX);

                    Color finalColor000111 = MixColor(colorMap[x + 0, z + 0], colorMap[x + 0, z + 1], colorMap[x + 1, z + 1]);
                    Color finalcolor001011 = MixColor(colorMap[x + 0, z + 0], colorMap[x + 1, z + 0], colorMap[x + 1, z + 1]);

                    colors[meshVertexIndex0 + 0] = finalColor000111;
                    colors[meshVertexIndex0 + 1] = finalColor000111;
                    colors[meshVertexIndex0 + 2] = finalColor000111;
                    colors[meshVertexIndex0 + 3] = finalcolor001011;
                    colors[meshVertexIndex0 + 4] = finalcolor001011;
                    colors[meshVertexIndex0 + 5] = finalcolor001011;
                }
            }
            mesh.colors = colors;
        }

        private static Color MixColor(Color c1, Color c2, Color c3)
        {
            Color result = c1;
            for (int i = 0; i < 3; i++)
            {
                result[i] = Mathf.Min(result[i], c2[i]);
                result[i] = Mathf.Min(result[i], c3[i]);
            }
            return result;
        }
    }
}