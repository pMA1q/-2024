using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_CameraAspectRatioSetter : MonoBehaviour
{
    public Camera mainCamera;      // ���C���J������ݒ�
    public Camera subCamera;       // �T�u�J������ݒ�
    public float mainCameraAspect = 16f / 9f;  // ���C���J�����̃A�X�y�N�g��
    public float subCameraAspect  =  9f /16f;  // �T�u�J�����̃A�X�y�N�g��

    void Start()
    {
        if (mainCamera != null)
        {
            mainCamera.aspect = mainCameraAspect;  // ���C���J�����̃A�X�y�N�g��ݒ�
        }

        if (subCamera != null)
        {
            subCamera.aspect = subCameraAspect;    // �T�u�J�����̃A�X�y�N�g��ݒ�
        }
    }
} 
