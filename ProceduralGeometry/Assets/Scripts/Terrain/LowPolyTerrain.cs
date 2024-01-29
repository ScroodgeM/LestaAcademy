using System.Collections.Generic;
using UnityEngine;

namespace Battlegrounds
{
    public class LowPolyTerrain : MonoBehaviour
    {
        [SerializeField] private float cellSize = 0.5f;
        [SerializeField] private Vector2 terrainSize;
        [SerializeField] private Material terrainMaterial;
        [SerializeField] private bool weldVertices;

        protected List<LowPolyTerrainChunk> chunks = new List<LowPolyTerrainChunk>();

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
            LowPolyTerrainChunk chunk = CreateChunk(0, 0, int.MaxValue);
            chunk.ApplyColorsAndGeometry(colorMapPixels, heights);
            chunks.Add(chunk);
        }

        protected virtual void GetDataAtCell(int x, int z, out float height, out Color color)
        {
            height = (Mathf.Abs(x - totalCellsX / 2) + Mathf.Abs(z - totalCellsZ / 2)) * 3f;
            color = new Color((float)x / totalCellsX, (float)z / totalCellsZ, 1f, 1f);
        }

        protected LowPolyTerrainChunk CreateChunk(int chunkIndexX, int chunkIndexZ, int chunkSize)
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

            return new LowPolyTerrainChunk(
                gameObject: chunkGO,
                meshFilter: meshFilter,
                meshCollider: meshCollider,
                terrainScale: terrainScale,
                chunkIndexX: chunkIndexX,
                chunkIndexZ: chunkIndexZ,
                chunkSize: chunkSize,
                totalCellsX: totalCellsX,
                totalCellsZ: totalCellsZ,
                weldVertices);

            Vector3 GetChunkPosition()
            {
                return new Vector3(chunkIndexX * terrainScale.x, 0f, chunkIndexZ * terrainScale.z) * chunkSize;
            }
        }
    }
}
