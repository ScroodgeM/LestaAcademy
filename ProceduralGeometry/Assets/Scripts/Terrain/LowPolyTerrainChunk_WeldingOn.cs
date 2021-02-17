using System;
using UnityEngine;
using UnityEngine.Assertions;

namespace Battlegrounds
{
    [Serializable]
    public class LowPolyTerrainChunk_WeldingOn : LowPolyTerrainChunk_Base
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

        public LowPolyTerrainChunk_WeldingOn(
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

            mesh = new Mesh { name = string.Format("Chunk x{0} z{1}", chunkIndexX, chunkIndexZ) };
            meshFilter.mesh = mesh;
            meshCollider.sharedMesh = mesh;

            Assert.IsTrue(cellsCountX > 0);
            Assert.IsTrue(cellsCountZ > 0);

            int vertexCount = (cellsCountX + 1) * (cellsCountZ + 1);
            int trianglesCount = 6 * cellsCountX * cellsCountZ;
            vertices = new Vector3[vertexCount];
            triangles = new int[trianglesCount];
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

                    int meshVertexIndex0 = GetVerticeIndex(localX + 0, localZ + 0);
                    int meshVertexIndex1 = GetVerticeIndex(localX + 0, localZ + 1);
                    int meshVertexIndex2 = GetVerticeIndex(localX + 1, localZ + 1);
                    int meshVertexIndex3 = GetVerticeIndex(localX + 0, localZ + 0);
                    int meshVertexIndex4 = GetVerticeIndex(localX + 1, localZ + 1);
                    int meshVertexIndex5 = GetVerticeIndex(localX + 1, localZ + 0);

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
                        int meshVertexIndex10 = 6 * (localX + localZ * cellsCountX);
                        int meshVertexIndex11 = meshVertexIndex10 + 1;
                        int meshVertexIndex12 = meshVertexIndex10 + 2;
                        int meshVertexIndex13 = meshVertexIndex10 + 3;
                        int meshVertexIndex14 = meshVertexIndex10 + 4;
                        int meshVertexIndex15 = meshVertexIndex10 + 5;

                        triangles[meshVertexIndex10] = meshVertexIndex0;
                        triangles[meshVertexIndex11] = meshVertexIndex1;
                        triangles[meshVertexIndex12] = meshVertexIndex2;
                        triangles[meshVertexIndex13] = meshVertexIndex3;
                        triangles[meshVertexIndex14] = meshVertexIndex4;
                        triangles[meshVertexIndex15] = meshVertexIndex5;
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
            for (int x = mapIndexXFrom; x <= mapIndexXTo; x++)
            {
                for (int z = mapIndexZFrom; z <= mapIndexZTo; z++)
                {
                    int localX = x - mapIndexXFrom;
                    int localZ = z - mapIndexZFrom;
                    colors[GetVerticeIndex(localX, localZ)] = colorMap[x, z];
                }
            }
            mesh.colors = colors;
        }

        private int GetVerticeIndex(int localX, int localZ)
        {
            return localX + localZ * (cellsCountX + 1);
        }
    }
}
