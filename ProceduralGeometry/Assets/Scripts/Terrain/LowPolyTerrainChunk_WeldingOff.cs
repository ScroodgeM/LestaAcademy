using System;
using UnityEngine;
using UnityEngine.Assertions;

namespace Battlegrounds
{
    [Serializable]
    public class LowPolyTerrainChunk_WeldingOff : LowPolyTerrainChunk_Base
    {
        private readonly MeshCollider meshCollider;
        private readonly Vector3 terrainScale;

        private readonly Mesh mesh;
        private readonly Vector3[] vertices;
        private readonly int[] triangles;
        private readonly Color[] colors;

        private bool trianglesApplied = false;

        private int cellsCountX { get { return (mapIndexXTo - mapIndexXFrom); } }
        private int cellsCountZ { get { return (mapIndexZTo - mapIndexZFrom); } }

        public LowPolyTerrainChunk_WeldingOff(
            GameObject gameObject,
            MeshFilter meshFilter,
            MeshCollider meshCollider,
            Vector3 terrainScale,
            int chunkIndexX,
            int chunkIndexZ,
            int chunkSize,
            int totalCellsX,
            int totalCellsZ) : base(gameObject, chunkIndexX, chunkIndexZ, chunkSize, totalCellsX, totalCellsZ)
        {
            this.meshCollider = meshCollider;
            this.terrainScale = terrainScale;

            mesh = new Mesh { name = string.Format("Chunk x{0} z{1}", chunkIndexX, chunkIndexZ), hideFlags = HideFlags.DontSave, };
            meshFilter.mesh = mesh;
            meshCollider.sharedMesh = mesh;

            Assert.IsTrue(cellsCountX > 0);
            Assert.IsTrue(cellsCountZ > 0);

            int vertexCount = 6 * cellsCountX * cellsCountZ;
            vertices = new Vector3[vertexCount];
            triangles = new int[vertexCount];
            colors = new Color[vertexCount];
        }

        internal override void ApplyGeometry(float[,] heights)
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
            if (!trianglesApplied)
            {
                mesh.triangles = triangles;
            }
            mesh.RecalculateNormals();
            meshCollider.sharedMesh = mesh;
            trianglesApplied = true;
        }

        internal override void ApplyColors(Color[,] colorMap)
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