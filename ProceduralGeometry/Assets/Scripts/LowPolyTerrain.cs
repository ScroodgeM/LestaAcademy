
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Battlegrounds
{
    public class LowPolyTerrain : MonoBehaviour
    {
        [Serializable]
        private struct TerrainLayer
        {
            public AnimationCurve heightsAffected;
            public float density;
            public float minMapping;
            public float maxMapping;
            public Gradient color;
        }

        [SerializeField] private TerrainLayer[] terrainLayers;
        [SerializeField] private float cellSize = 0.5f;
        [SerializeField] private int chunkSize = 32;

        [Space]

        [SerializeField] private Vector2 terrainSize;
        [SerializeField] private float waterHeight = -1;

        [Space]

        [SerializeField] private Material terrainMaterial;
        [SerializeField] private Color holeColor;

        [Space]

        private List<LowPolyTerrainChunk> chunks = new List<LowPolyTerrainChunk>();

        private int totalCellsX;
        private int totalCellsZ;
        private int totalChunksX;
        private int totalChunksZ;
        private Vector3 terrainScale;

        private float[,] heights;
        private Color[,] colorMapPixels;

        private void Awake()
        {
            GenerateTerrain();
        }

        public void Clear()
        {
            foreach (LowPolyTerrainChunk chunk in chunks)
            {
                if (Application.isPlaying)
                {
                    Destroy(chunk.gameObject);
                }
                else
                {
                    DestroyImmediate(chunk.gameObject);
                }
            }
            chunks.Clear();
        }

        public void GenerateTerrain()
        {
            Clear();

            totalCellsX = Mathf.FloorToInt(terrainSize.x / cellSize);
            totalCellsZ = Mathf.FloorToInt(terrainSize.y / cellSize);

            totalChunksX = totalCellsX / chunkSize;
            if (chunkSize * totalChunksX < totalCellsX)
            {
                totalChunksX++;
            }

            totalChunksZ = totalCellsZ / chunkSize;
            if (chunkSize * totalChunksZ < totalCellsZ)
            {
                totalChunksZ++;
            }

            terrainScale = new Vector3(
                terrainSize.x / totalCellsX,
                0f,
                terrainSize.y / totalCellsZ);

            heights = new float[totalCellsX + 1, totalCellsZ + 1];
            colorMapPixels = new Color[totalCellsX + 1, totalCellsZ + 1];

            for (int x = 0; x <= totalCellsX; x++)
            {
                for (int z = 0; z <= totalCellsZ; z++)
                {
                    GetDataAtCell(x, z, out float height, out Color color);

                    heights[x, z] = height;
                    colorMapPixels[x, z] = color;
                }
            }

            for (int x = 0; x < totalChunksX; x++)
            {
                for (int z = 0; z < totalChunksZ; z++)
                {
                    var chunk = CreateChunk(x, z);
                    chunk.ApplyGeometry(heights);
                    chunk.ApplyColors(colorMapPixels);
                    chunks.Add(chunk);
                }
            }
        }

        private void GetDataAtCell(int x, int z, out float height, out Color color)
        {
            height = 0;
            color = Color.white;

            foreach (TerrainLayer terrainLayer in terrainLayers)
            {
                float multiplier = terrainLayer.heightsAffected.Evaluate(height);

                float layerValue = Mathf.PerlinNoise(x * terrainLayer.density * terrainScale.x, z * terrainLayer.density * terrainScale.z);
                height += Mathf.Lerp(terrainLayer.minMapping, terrainLayer.maxMapping, layerValue * multiplier);

                Color newColor = terrainLayer.color.Evaluate(layerValue);
                color = Color.Lerp(color, newColor, newColor.a * multiplier);
            }
        }

        internal void Deformate(DeformationModes deformationMode, Vector3 center, float radius, float epicenterDeltaHeight)
        {
            float holeFlattening = epicenterDeltaHeight / radius;

            center = transform.InverseTransformPoint(center);
            radius = radius / (transform.lossyScale.magnitude / Mathf.Sqrt(3f));

            int xMin = Mathf.CeilToInt((center.x - radius) / terrainScale.x);
            int xMax = Mathf.FloorToInt((center.x + radius) / terrainScale.x);
            int zMin = Mathf.CeilToInt((center.z - radius) / terrainScale.z);
            int zMax = Mathf.FloorToInt((center.z + radius) / terrainScale.z);

            for (int x = xMin; x <= xMax; x++)
            {
                if (x >= 0 && x <= totalCellsX)
                {
                    float worldX = x * terrainScale.x;
                    for (int z = zMin; z <= zMax; z++)
                    {
                        if (z >= 0 && z <= totalCellsZ)
                        {
                            float worldZ = z * terrainScale.z;
                            float r2 = radius * radius;
                            float x2 = (center.x - worldX) * (center.x - worldX);
                            float z2 = (center.z - worldZ) * (center.z - worldZ);
                            if (r2 > x2 + z2)
                            {
                                float oldWorldHeightInPoint = heights[x, z];

                                float maxDeltaHeight = (Mathf.Cos(Mathf.Sqrt(x2 + z2) / radius * Mathf.PI) * 0.5f + 0.5f) * radius * holeFlattening;

                                float deltaHeight = 0f;
                                switch (deformationMode)
                                {
                                    case DeformationModes.Add:
                                        deltaHeight = Mathf.Max(Mathf.Min(center.y - oldWorldHeightInPoint, 0f) + maxDeltaHeight, 0f);
                                        break;
                                    case DeformationModes.Remove:
                                        deltaHeight = Mathf.Min(Mathf.Max(center.y - oldWorldHeightInPoint, 0f) - maxDeltaHeight, 0f);
                                        break;
                                }
                                if (!Mathf.Approximately(deltaHeight, 0f))
                                {
                                    // 1 for zero height, 0.5 for -1m, 0.33 for -2m, etc
                                    float unaffectedEarthFactor = 1 / (1 + Mathf.Abs(deltaHeight));
                                    colorMapPixels[x, z] = Color.Lerp(holeColor, colorMapPixels[x, z], unaffectedEarthFactor);
                                    heights[x, z] = oldWorldHeightInPoint + deltaHeight;
                                }
                            }
                        }
                    }
                }
            }
            UpdateChunks(xMin, xMax, zMin, zMax);
        }

        internal void ApplyNewHeightsMap(float[,] heightsMap, Color[,] colorsMap)
        {
            heights = heightsMap;
            colorMapPixels = colorsMap;
            UpdateChunks(int.MinValue, int.MaxValue, int.MinValue, int.MaxValue);
        }

        private void UpdateChunks(int xMin, int xMax, int zMin, int zMax)
        {
            for (int i = 0; i < chunks.Count; i++)
            {
                if (chunks[i].IsInRange(xMin, xMax, zMin, zMax))
                {
                    chunks[i].ApplyGeometry(heights);
                    chunks[i].ApplyColors(colorMapPixels);
                }
            }
        }

        private LowPolyTerrainChunk CreateChunk(int chunkIndexX, int chunkIndexZ)
        {
            GameObject chunkGO = new GameObject
            {
                name = string.Format("Chunk x{0} z{1}", chunkIndexX, chunkIndexZ),
                layer = transform.gameObject.layer,
                hideFlags = HideFlags.DontSave,
            };

            chunkGO.transform.SetParent(transform, false);
            chunkGO.transform.localPosition = GetChunkPosition(chunkIndexX, chunkIndexZ);
            chunkGO.transform.localRotation = Quaternion.identity;
            chunkGO.transform.localScale = Vector3.one;

            MeshRenderer meshRenderer = chunkGO.AddComponent<MeshRenderer>();
            meshRenderer.material = terrainMaterial;

            MeshFilter meshFilter = chunkGO.AddComponent<MeshFilter>();
            MeshCollider meshCollider = chunkGO.AddComponent<MeshCollider>();

            return new LowPolyTerrainChunk(
                gameObject: chunkGO,
                meshFilter: meshFilter,
                meshCollider: meshCollider,
                terrainScale: terrainScale,
                chunkIndexX: chunkIndexX,
                chunkIndexZ: chunkIndexZ,
                chunkSize: chunkSize,
                totalCellsX: totalCellsX,
                totalCellsZ: totalCellsZ);
        }

        private Vector3 GetChunkPosition(int chunkIndexX, int chunkIndexZ)
        {
            return new Vector3(chunkIndexX * terrainScale.x, 0f, chunkIndexZ * terrainScale.z) * chunkSize;
        }
    }
}
