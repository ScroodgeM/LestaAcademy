
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

        [Space]

        [SerializeField] private Material terrainMaterial;

        [Space]

        protected List<LowPolyTerrainChunk> chunks = new List<LowPolyTerrainChunk>();

        protected int totalCellsX;
        protected int totalCellsZ;
        private int totalChunksX;
        private int totalChunksZ;
        protected Vector3 terrainScale;

        protected float[,] heights;
        protected Color[,] colorMapPixels;

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
