
using System;
using UnityEngine;

namespace Battlegrounds
{
    public class LowPolyTerrain_Perlin : LowPolyTerrain
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

        protected override void GetDataAtCell(int x, int z, out float height, out Color color)
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
    }
}
