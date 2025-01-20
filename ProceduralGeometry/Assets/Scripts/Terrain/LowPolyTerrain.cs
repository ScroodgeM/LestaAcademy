using System;
using System.Collections.Generic;
using UnityEngine;

namespace Battlegrounds
{
    [ExecuteInEditMode]
    public class LowPolyTerrain : MonoBehaviour
    {
        [SerializeField] protected float cellSize = 1f;
        [SerializeField] protected int cellsCount = 25;
        [SerializeField] private float uvScale = 1f;
        [SerializeField] private Material terrainMaterial;
        [SerializeField] private bool weldVertices;
        [SerializeField] private bool continuousGenerationInEditor;

        protected readonly List<LowPolyTerrainChunk> chunks = new List<LowPolyTerrainChunk>();

        [Obsolete] protected int totalCellsX => cellsCount;
        [Obsolete] protected int totalCellsZ => cellsCount;
        [Obsolete] protected Vector3 terrainScale => cellSize * Vector3.one;

        protected float[,] heights;
        protected Color[,] colorMapPixels;

        private void Awake()
        {
            if (Application.isPlaying == true)
            {
                GenerateTerrain();
            }
        }

        private void Update()
        {
            if (continuousGenerationInEditor == true && Application.isPlaying == false)
            {
                GenerateTerrain();
            }
        }

        public void Clear()
        {
            foreach (LowPolyTerrainChunk chunk in chunks)
            {
                if (Application.isPlaying == true)
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
            heights = new float[cellsCount + 1, cellsCount + 1];
            colorMapPixels = new Color[cellsCount + 1, cellsCount + 1];

            for (int x = 0; x <= cellsCount; x++)
            {
                for (int z = 0; z <= cellsCount; z++)
                {
                    GetDataAtCell(x, z, out float height, out Color color);

                    heights[x, z] = height;
                    colorMapPixels[x, z] = color;
                }
            }
        }

        protected virtual void GenerateChunks()
        {
            LowPolyTerrainChunk chunk = CreateChunk(0, 0, int.MaxValue);
            chunk.ApplyColorsAndGeometry(colorMapPixels, heights);
            chunks.Add(chunk);
        }

        protected virtual void GetDataAtCell(int x, int z, out float height, out Color color)
        {
            Vector2 worldPosition2D = HeightIndexToWorldPosition2D(x, z);
            height = worldPosition2D.sqrMagnitude * 0.005f;
            color = new Color((float)x / cellsCount, (float)z / cellsCount, height / 50f, 1f);
        }

        protected LowPolyTerrainChunk CreateChunk(int chunkIndexX, int chunkIndexZ, int chunkSize)
        {
            GameObject chunkGO = new GameObject
            {
                name = $"Chunk x{chunkIndexX} z{chunkIndexZ}",
                layer = gameObject.layer,
                hideFlags = HideFlags.DontSave,
            };

            Vector2 chunkPosition2D = HeightIndexToWorldPosition2D(chunkIndexX * chunkSize, chunkIndexZ * chunkSize);

            chunkGO.transform.SetParent(transform, false);
            chunkGO.transform.localPosition = new Vector3(chunkPosition2D.x, 0f, chunkPosition2D.y);
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
                cellSize: cellSize,
                cellsCount: cellsCount,
                uvScale: uvScale,
                chunkIndexX: chunkIndexX,
                chunkIndexZ: chunkIndexZ,
                chunkSize: chunkSize,
                weldVertices: weldVertices,
                heightIndexToWorldPosition2D: HeightIndexToWorldPosition2D);
        }

        protected Vector2 HeightIndexToWorldPosition2D(int heightIndexX, int heightIndexZ)
        {
            return HeightIndexToWorldPosition2D(new Vector2Int(heightIndexX, heightIndexZ));
        }

        protected Vector2 HeightIndexToWorldPosition2D(Vector2Int heightIndex)
        {
            return new Vector2(heightIndex.x - cellsCount * 0.5f, heightIndex.y - cellsCount * 0.5f) * cellSize;
        }
    }
}
