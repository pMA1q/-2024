using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_CommonData : MonoBehaviour
{
    //無発展フラグ
    private bool IsNoDevelopment = false;
    public bool NoDevelpment
    {
        set { IsNoDevelopment = value; }
        get { return IsNoDevelopment; }
    }
}
