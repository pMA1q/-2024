using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_EllipticalTerrain : MonoBehaviour
{
    public Terrain terrain;
    public float ellipseWidth = 50f; // 楕円の横幅
    public float ellipseHeight = 30f; // 楕円の縦幅

    void Start()
    {
        TerrainData terrainData = terrain.terrainData;
        int resolution = terrainData.heightmapResolution;
        float[,] heights = terrainData.GetHeights(0, 0, resolution, resolution);

        // 楕円形にする
        for (int y = 0; y < resolution; y++)
        {
            for (int x = 0; x < resolution; x++)
            {
                float normalizedX = (float)x / resolution * 2 - 1; // -1 〜 1 に正規化
                float normalizedY = (float)y / resolution * 2 - 1;

                // 楕円形の範囲内を判定
                float ellipseValue = (normalizedX * normalizedX) / (ellipseWidth * ellipseWidth)
                                   + (normalizedY * normalizedY) / (ellipseHeight * ellipseHeight);

                if (ellipseValue > 1)
                {
                    heights[y, x] = 0; // 楕円外は高さを0にする
                }
            }
        }

        // 更新
        terrainData.SetHeights(0, 0, heights);
    }
}
