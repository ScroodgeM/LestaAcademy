using System;
using UnityEngine;

namespace Battlegrounds
{
    public abstract class LowPolyTerrainChunk_Base
    {
        internal readonly GameObject gameObject;

        internal readonly int totalCellsX;
        internal readonly int totalCellsZ;

        internal readonly int mapIndexXFrom;
        internal readonly int mapIndexXTo;
        internal readonly int mapIndexZFrom;
        internal readonly int mapIndexZTo;

        internal LowPolyTerrainChunk_Base(GameObject gameObject, int chunkIndexX, int chunkIndexZ, int chunkSize, int totalCellsX, int totalCellsZ)
        {
            this.gameObject = gameObject;

            this.totalCellsX = totalCellsX;
            this.totalCellsZ = totalCellsZ;

            this.mapIndexXFrom = chunkIndexX * chunkSize;
            this.mapIndexXTo = Mathf.Min((chunkIndexX + 1) * chunkSize, totalCellsX);
            this.mapIndexZFrom = chunkIndexZ * chunkSize;
            this.mapIndexZTo = Mathf.Min((chunkIndexZ + 1) * chunkSize, totalCellsZ);
        }

        internal abstract void ApplyGeometry(float[,] heights);

        internal abstract void ApplyColors(Color[,] colorMap);

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
    }
}
