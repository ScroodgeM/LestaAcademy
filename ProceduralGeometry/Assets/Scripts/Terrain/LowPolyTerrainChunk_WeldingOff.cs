using System;
using UnityEngine;

namespace Battlegrounds
{
    [Serializable]
    public class LowPolyTerrainChunk_WeldingOff : LowPolyTerrainChunk_Base
    {
        public LowPolyTerrainChunk_WeldingOff(
            GameObject gameObject,
            MeshFilter meshFilter,
            MeshCollider meshCollider,
            Vector3 terrainScale,
            int chunkIndexX,
            int chunkIndexZ,
            int chunkSize,
            int totalCellsX,
            int totalCellsZ)
            : base(gameObject, meshFilter, meshCollider, terrainScale, chunkIndexX, chunkIndexZ, chunkSize, totalCellsX, totalCellsZ, false)
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

                    Color finalColor000111 = MixColor(colorMap[x + 0, z + 0], colorMap[x + 0, z + 1], colorMap[x + 1, z + 1]);
                    Color finalColor001011 = MixColor(colorMap[x + 0, z + 0], colorMap[x + 1, z + 0], colorMap[x + 1, z + 1]);

                    colors[meshVertexIndex0] = finalColor000111;
                    colors[meshVertexIndex1] = finalColor000111;
                    colors[meshVertexIndex2] = finalColor000111;
                    colors[meshVertexIndex3] = finalColor001011;
                    colors[meshVertexIndex4] = finalColor001011;
                    colors[meshVertexIndex5] = finalColor001011;
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
