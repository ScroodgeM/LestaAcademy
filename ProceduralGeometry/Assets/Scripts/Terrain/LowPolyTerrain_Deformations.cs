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

            Vector2Int min = WorldPosition2DToHeightIndex(new Vector2(center.x - radius, center.z - radius));
            Vector2Int max = WorldPosition2DToHeightIndex(new Vector2(center.x + radius, center.z + radius));

            for (int x = min.x; x <= max.x; x++)
            {
                if (x >= 0 && x <= cellsCount)
                {
                    for (int z = min.y; z <= max.y; z++)
                    {
                        if (z >= 0 && z <= cellsCount)
                        {
                            Vector2 worldPosition2D = HeightIndexToWorldPosition2D(x, z);
                            float r2 = radius * radius;
                            float x2 = (center.x - worldPosition2D.x) * (center.x - worldPosition2D.x);
                            float z2 = (center.z - worldPosition2D.y) * (center.z - worldPosition2D.y);
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

            UpdateChunks(min.x, max.x, min.y, max.y);
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
