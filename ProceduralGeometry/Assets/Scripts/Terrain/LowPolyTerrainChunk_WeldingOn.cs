using System;
using UnityEngine;

namespace Battlegrounds
{
    [Serializable]
    public class LowPolyTerrainChunk_WeldingOn : LowPolyTerrainChunk_Base
    {
        public LowPolyTerrainChunk_WeldingOn(
            GameObject gameObject,
            MeshFilter meshFilter,
            MeshCollider meshCollider,
            Vector3 terrainScale,
            int chunkIndexX,
            int chunkIndexZ,
            int chunkSize,
            int totalCellsX,
            int totalCellsZ)
            : base(gameObject, meshFilter, meshCollider, terrainScale, chunkIndexX, chunkIndexZ, chunkSize, totalCellsX, totalCellsZ, true)
        {
        }

        internal override void ApplyColorsAndGeometry(Color[,] colorMap, float[,] heights)
        {
            for (int x = mapIndexXFrom; x < mapIndexXTo; x++)
            {
                for (int z = mapIndexZFrom; z < mapIndexZTo; z++)
                {
                    int localX = x - mapIndexXFrom;
                    int localZ = z - mapIndexZFrom;

                    int vertex00index = GetVerticeIndex(localX + 0, localZ + 0);
                    int vertex01index = GetVerticeIndex(localX + 0, localZ + 1);
                    int vertex11index = GetVerticeIndex(localX + 1, localZ + 1);
                    int vertex10index = GetVerticeIndex(localX + 1, localZ + 0);

                    vertices[vertex00index] = new Vector3((localX + 0) * terrainScale.x, heights[x + 0, z + 0], (localZ + 0) * terrainScale.z);
                    vertices[vertex01index] = new Vector3((localX + 0) * terrainScale.x, heights[x + 0, z + 1], (localZ + 1) * terrainScale.z);
                    vertices[vertex11index] = new Vector3((localX + 1) * terrainScale.x, heights[x + 1, z + 1], (localZ + 1) * terrainScale.z);
                    vertices[vertex10index] = new Vector3((localX + 1) * terrainScale.x, heights[x + 1, z + 0], (localZ + 0) * terrainScale.z);

                    if (!trianglesApplied)
                    {
                        int index = 6 * (localX + localZ * cellsCountX);

                        triangles[index + 0] = vertex00index;
                        triangles[index + 1] = vertex01index;
                        triangles[index + 2] = vertex11index;

                        triangles[index + 3] = vertex00index;
                        triangles[index + 4] = vertex11index;
                        triangles[index + 5] = vertex10index;
                    }

                    colors[GetVerticeIndex(localX, localZ)] = colorMap[x, z];
                }
            }

            mesh.vertices = vertices;
            if (!trianglesApplied)
            {
                mesh.triangles = triangles;
            }

            mesh.colors = colors;
            mesh.RecalculateBounds();
            mesh.RecalculateNormals();
            meshCollider.sharedMesh = mesh;
            trianglesApplied = true;
        }

        private int GetVerticeIndex(int localX, int localZ)
        {
            return localX + localZ * (cellsCountX + 1);
        }
    }
}
