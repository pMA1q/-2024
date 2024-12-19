using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_EllipticalTerrain : MonoBehaviour
{
    public Terrain terrain;
    public float ellipseWidth = 50f; // �ȉ~�̉���
    public float ellipseHeight = 30f; // �ȉ~�̏c��

    void Start()
    {
        TerrainData terrainData = terrain.terrainData;
        int resolution = terrainData.heightmapResolution;
        float[,] heights = terrainData.GetHeights(0, 0, resolution, resolution);

        // �ȉ~�`�ɂ���
        for (int y = 0; y < resolution; y++)
        {
            for (int x = 0; x < resolution; x++)
            {
                float normalizedX = (float)x / resolution * 2 - 1; // -1 �` 1 �ɐ��K��
                float normalizedY = (float)y / resolution * 2 - 1;

                // �ȉ~�`�͈͓̔��𔻒�
                float ellipseValue = (normalizedX * normalizedX) / (ellipseWidth * ellipseWidth)
                                   + (normalizedY * normalizedY) / (ellipseHeight * ellipseHeight);

                if (ellipseValue > 1)
                {
                    heights[y, x] = 0; // �ȉ~�O�͍�����0�ɂ���
                }
            }
        }

        // �X�V
        terrainData.SetHeights(0, 0, heights);
    }
}
