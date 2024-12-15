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
    [Header("ポジション補間")]
    public List<Vector3> positions; 
    [Header("回転補間")]
    public List<Vector3> lotations;

    [Header("移動スピード")]
    public float moveSpeed;
    [Header("回転スピード")]
    public float rotateSpeed;
}