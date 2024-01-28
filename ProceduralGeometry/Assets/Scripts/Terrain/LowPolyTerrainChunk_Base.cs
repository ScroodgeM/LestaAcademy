using UnityEngine;
using UnityEngine.Assertions;

namespace Battlegrounds
{
    public abstract class LowPolyTerrainChunk_Base
    {
        internal readonly GameObject gameObject;

        internal readonly int mapIndexXFrom;
        internal readonly int mapIndexXTo;
        internal readonly int mapIndexZFrom;
        internal readonly int mapIndexZTo;

        protected readonly MeshCollider meshCollider;
        protected readonly Vector3 terrainScale;

        protected readonly Mesh mesh;
        protected readonly Vector3[] vertices;
        protected readonly int[] triangles;
        protected readonly Color[] colors;

        protected bool trianglesApplied = false;

        protected int cellsCountX => mapIndexXTo - mapIndexXFrom;
        protected int cellsCountZ => mapIndexZTo - mapIndexZFrom;

        internal LowPolyTerrainChunk_Base(GameObject gameObject,
            MeshFilter meshFilter,
            MeshCollider meshCollider,
            Vector3 terrainScale,
            int chunkIndexX,
            int chunkIndexZ,
            int chunkSize,
            int totalCellsX,
            int totalCellsZ,
            bool weldVertices)
        {
            this.gameObject = gameObject;

            this.mapIndexXFrom = chunkIndexX * chunkSize;
            this.mapIndexXTo = Mathf.Min((chunkIndexX + 1) * chunkSize, totalCellsX);
            this.mapIndexZFrom = chunkIndexZ * chunkSize;
            this.mapIndexZTo = Mathf.Min((chunkIndexZ + 1) * chunkSize, totalCellsZ);

            this.meshCollider = meshCollider;
            this.terrainScale = terrainScale;

            mesh = new Mesh { name = $"Chunk x{chunkIndexX} z{chunkIndexZ}", hideFlags = HideFlags.DontSave, };
            meshFilter.mesh = mesh;
            meshCollider.sharedMesh = mesh;

            Assert.IsTrue(cellsCountX > 0);
            Assert.IsTrue(cellsCountZ > 0);

            int vertexCount = weldVertices == true ? (cellsCountX + 1) * (cellsCountZ + 1) : 6 * cellsCountX * cellsCountZ;
            int trianglesCount = 6 * cellsCountX * cellsCountZ;

            vertices = new Vector3[vertexCount];
            triangles = new int[trianglesCount];
            colors = new Color[vertexCount];
        }

        internal abstract void ApplyColorsAndGeometry(Color[,] colorMap, float[,] heights);

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
