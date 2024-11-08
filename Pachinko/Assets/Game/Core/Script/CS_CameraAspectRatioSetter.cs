using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_CameraAspectRatioSetter : MonoBehaviour
{
    public Camera mainCamera;      // メインカメラを設定
    public Camera subCamera;       // サブカメラを設定
    public float mainCameraAspect = 16f / 9f;  // メインカメラのアスペクト比
    public float subCameraAspect  =  9f /16f;  // サブカメラのアスペクト比

    void Start()
    {
        if (mainCamera != null)
        {
            mainCamera.aspect = mainCameraAspect;  // メインカメラのアスペクト比設定
        }

        if (subCamera != null)
        {
            subCamera.aspect = subCameraAspect;    // サブカメラのアスペクト比設定
        }
    }
} 
