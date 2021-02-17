
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
        [SerializeField] private Vector2 terrainSize;
        [SerializeField] private Material terrainMaterial;
        [SerializeField] private bool weldVertices;

        protected List<LowPolyTerrainChunk_Base> chunks = new List<LowPolyTerrainChunk_Base>();

        protected int totalCellsX;
        protected int totalCellsZ;

        protected Vector3 terrainScale;

        protected float[,] heights;
        protected Color[,] colorMapPixels;

        private void Awake()
        {
            GenerateTerrain();
        }

        public void Clear()
        {
            foreach (LowPolyTerrainChunk_Base chunk in chunks)
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
            GenerateMap();
            GenerateChunks();
        }

        private void GenerateMap()
        {
            totalCellsX = Mathf.FloorToInt(terrainSize.x / cellSize);
            totalCellsZ = Mathf.FloorToInt(terrainSize.y / cellSize);

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
        }

        protected virtual void GenerateChunks()
        {
            LowPolyTerrainChunk_Base chunk = CreateChunk(0, 0, int.MaxValue);
            chunk.ApplyGeometry(heights);
            chunk.ApplyColors(colorMapPixels);
            chunks.Add(chunk);
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

        protected LowPolyTerrainChunk_Base CreateChunk(int chunkIndexX, int chunkIndexZ, int chunkSize)
        {
            GameObject chunkGO = new GameObject
            {
                name = $"Chunk x{chunkIndexX} z{chunkIndexZ}",
                layer = gameObject.layer,
                hideFlags = HideFlags.DontSave,
            };

            chunkGO.transform.SetParent(transform, false);
            chunkGO.transform.localPosition = GetChunkPosition();
            chunkGO.transform.localRotation = Quaternion.identity;
            chunkGO.transform.localScale = Vector3.one;

            MeshRenderer meshRenderer = chunkGO.AddComponent<MeshRenderer>();
            meshRenderer.material = terrainMaterial;

            MeshFilter meshFilter = chunkGO.AddComponent<MeshFilter>();
            MeshCollider meshCollider = chunkGO.AddComponent<MeshCollider>();

            if (weldVertices == true)
            {
                return new LowPolyTerrainChunk_WeldingOn(
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
            else
            {
                return new LowPolyTerrainChunk_WeldingOff(
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

            Vector3 GetChunkPosition()
            {
                return new Vector3(chunkIndexX * terrainScale.x, 0f, chunkIndexZ * terrainScale.z) * chunkSize;
            }
        }
    }
}
