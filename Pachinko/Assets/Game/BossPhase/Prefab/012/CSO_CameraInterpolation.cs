using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CameraWork", menuName = "Camera/CSO_CameraPositions", order = 1)]
public class CSO_CameraInterpolation : ScriptableObject
{
    public List<CameraInfomation> inofomations;
}
[System.Serializable]
public class CameraInfomation
{
    [Header("�|�W�V�������")]
    public List<Vector3> positions; 
    [Header("��]���")]
    public List<Vector3> lotations;

    [Header("�ړ��X�s�[�h")]
    public float moveSpeed;
    [Header("��]�X�s�[�h")]
    public float rotateSpeed;
}