using UnityEngine;

namespace Battlegrounds
{
    public class LowPolyTerrain_Chunks : LowPolyTerrain_Perlin
    {
        [SerializeField] private int chunkSize = 8;

        private int totalChunksX;
        private int totalChunksZ;

        protected override void GenerateChunks()
        {
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

            for (int x = 0; x < totalChunksX; x++)
            {
                for (int z = 0; z < totalChunksZ; z++)
                {
                    var chunk = CreateChunk(x, z, chunkSize);
                    chunk.ApplyColorsAndGeometry(colorMapPixels, heights);
                    chunks.Add(chunk);
                }
            }
        }
    }
}
