using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_CommonData : MonoBehaviour
{
    public static string Obj3D_RenderCamera = "FCamera";
    //�����W�t���O
    private bool IsNoDevelopment = false;
    public bool NoDevelpment
    {
        set { IsNoDevelopment = value; }
        get { return IsNoDevelopment; }
    }
}
