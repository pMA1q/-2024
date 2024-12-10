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
    [Header("ƒ|ƒWƒVƒ‡ƒ“•âŠÔ")]
    public List<Vector3> positions; 
    [Header("‰ñ“]•âŠÔ")]
    public List<Vector3> lotations;

}