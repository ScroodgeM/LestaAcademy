using UnityEngine;

namespace Battlegrounds
{
    public class LowPolyTerrain_Deformations : LowPolyTerrain_Chunks
    {
        [SerializeField] private Color holeColor;

        internal void Deformate(DeformationModes deformationMode, Vector3 center, float radius, float epicenterDeltaHeight)
        {
            float holeFlattening = epicenterDeltaHeight / radius;

            center = transform.InverseTransformPoint(center);
            radius /= (transform.lossyScale.magnitude / Mathf.Sqrt(3f));

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

        private void UpdateChunks(int xMin, int xMax, int zMin, int zMax)
        {
            for (int i = 0; i < chunks.Count; i++)
            {
                if (chunks[i].IsInRange(xMin, xMax, zMin, zMax))
                {
                    chunks[i].ApplyColorsAndGeometry(colorMapPixels, heights);
                }
            }
        }
    }
}
