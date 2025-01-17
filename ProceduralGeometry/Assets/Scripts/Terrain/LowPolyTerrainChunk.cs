﻿using System;
using UnityEngine;
using UnityEngine.Assertions;

namespace Battlegrounds
{
    public class LowPolyTerrainChunk
    {
        internal readonly GameObject gameObject;

        private readonly int mapIndexXFrom;
        private readonly int mapIndexXTo;
        private readonly int mapIndexZFrom;
        private readonly int mapIndexZTo;

        private readonly MeshCollider meshCollider;
        private readonly float cellSize;
        private readonly float uvScale;
        private readonly bool weldVertices;
        private readonly Func<int, int, Vector2> heightIndexToWorldPosition2D;

        private readonly Mesh mesh;
        private readonly Vector3[] vertices;
        private readonly int[] triangles;
        private readonly Vector2[] uv;
        private readonly Color[] colors;

        private bool trianglesApplied = false;

        private int cellsCountX => mapIndexXTo - mapIndexXFrom;
        private int cellsCountZ => mapIndexZTo - mapIndexZFrom;

        internal LowPolyTerrainChunk(GameObject gameObject,
            MeshFilter meshFilter,
            MeshCollider meshCollider,
            float cellSize,
            int cellsCount,
            float uvScale,
            int chunkIndexX,
            int chunkIndexZ,
            int chunkSize,
            bool weldVertices,
            Func<int, int, Vector2> heightIndexToWorldPosition2D)
        {
            this.gameObject = gameObject;

            this.mapIndexXFrom = chunkIndexX * chunkSize;
            this.mapIndexXTo = Mathf.Min((chunkIndexX + 1) * chunkSize, cellsCount);
            this.mapIndexZFrom = chunkIndexZ * chunkSize;
            this.mapIndexZTo = Mathf.Min((chunkIndexZ + 1) * chunkSize, cellsCount);

            this.meshCollider = meshCollider;
            this.cellSize = cellSize;
            this.uvScale = uvScale;
            this.weldVertices = weldVertices;
            this.heightIndexToWorldPosition2D = heightIndexToWorldPosition2D;

            mesh = new Mesh { name = $"Chunk x{chunkIndexX} z{chunkIndexZ}", hideFlags = HideFlags.DontSave, };
            meshFilter.mesh = mesh;
            meshCollider.sharedMesh = mesh;

            Assert.IsTrue(cellsCountX > 0);
            Assert.IsTrue(cellsCountZ > 0);

            int vertexCount = weldVertices == true ? ((cellsCountX + 1) * (cellsCountZ + 1)) : (6 * cellsCountX * cellsCountZ);

            vertices = new Vector3[vertexCount];
            triangles = new int[6 * cellsCountX * cellsCountZ];
            uv = new Vector2[vertexCount];
            colors = new Color[vertexCount];
        }

        internal void ApplyColorsAndGeometry(Color[,] colorMap, float[,] heights)
        {
            for (int x = mapIndexXFrom; x < mapIndexXTo; x++)
            {
                for (int z = mapIndexZFrom; z < mapIndexZTo; z++)
                {
                    int localX = x - mapIndexXFrom;
                    int localZ = z - mapIndexZFrom;

                    Vector3 vertex00 = new Vector3((localX + 0) * cellSize, heights[x + 0, z + 0], (localZ + 0) * cellSize);
                    Vector3 vertex01 = new Vector3((localX + 0) * cellSize, heights[x + 0, z + 1], (localZ + 1) * cellSize);
                    Vector3 vertex10 = new Vector3((localX + 1) * cellSize, heights[x + 1, z + 0], (localZ + 0) * cellSize);
                    Vector3 vertex11 = new Vector3((localX + 1) * cellSize, heights[x + 1, z + 1], (localZ + 1) * cellSize);

                    if (weldVertices == true)
                    {
                        int vertex00index = GetVertexIndex(localX + 0, localZ + 0);
                        int vertex01index = GetVertexIndex(localX + 0, localZ + 1);
                        int vertex11index = GetVertexIndex(localX + 1, localZ + 1);
                        int vertex10index = GetVertexIndex(localX + 1, localZ + 0);

                        vertices[vertex00index] = vertex00;
                        vertices[vertex01index] = vertex01;
                        vertices[vertex11index] = vertex11;
                        vertices[vertex10index] = vertex10;

                        if (!trianglesApplied)
                        {
                            int index = 6 * (localX + localZ * cellsCountX);

                            triangles[index + 0] = vertex00index;
                            triangles[index + 1] = vertex01index;
                            triangles[index + 2] = vertex11index;

                            triangles[index + 3] = vertex00index;
                            triangles[index + 4] = vertex11index;
                            triangles[index + 5] = vertex10index;

                            uv[vertex00index] = heightIndexToWorldPosition2D(x + 0, z + 0) * uvScale;
                            uv[vertex01index] = heightIndexToWorldPosition2D(x + 0, z + 1) * uvScale;
                            uv[vertex11index] = heightIndexToWorldPosition2D(x + 1, z + 1) * uvScale;
                            uv[vertex10index] = heightIndexToWorldPosition2D(x + 1, z + 0) * uvScale;
                        }

                        colors[vertex00index] = colorMap[x + 0, z + 0];
                        colors[vertex01index] = colorMap[x + 0, z + 1];
                        colors[vertex11index] = colorMap[x + 1, z + 1];
                        colors[vertex10index] = colorMap[x + 1, z + 0];

                        int GetVertexIndex(int localX, int localZ)
                        {
                            return localX + localZ * (cellsCountX + 1);
                        }
                    }
                    else
                    {
                        int meshVertexIndex0 = 6 * (localX + localZ * cellsCountX);
                        int meshVertexIndex1 = meshVertexIndex0 + 1;
                        int meshVertexIndex2 = meshVertexIndex0 + 2;
                        int meshVertexIndex3 = meshVertexIndex0 + 3;
                        int meshVertexIndex4 = meshVertexIndex0 + 4;
                        int meshVertexIndex5 = meshVertexIndex0 + 5;

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

                            uv[meshVertexIndex0] = new Vector2(x + 0, z + 0);
                            uv[meshVertexIndex1] = new Vector2(x + 0, z + 1);
                            uv[meshVertexIndex2] = new Vector2(x + 1, z + 1);
                            uv[meshVertexIndex3] = new Vector2(x + 0, z + 0);
                            uv[meshVertexIndex4] = new Vector2(x + 1, z + 1);
                            uv[meshVertexIndex5] = new Vector2(x + 1, z + 0);
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
            }

            mesh.vertices = vertices;
            if (!trianglesApplied)
            {
                mesh.triangles = triangles;
                mesh.uv = uv;
            }

            mesh.colors = colors;
            mesh.RecalculateBounds();
            mesh.RecalculateNormals();
            meshCollider.sharedMesh = mesh;
            trianglesApplied = true;
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
