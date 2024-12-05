using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_CommonData : MonoBehaviour
{
    //–³”­“Wƒtƒ‰ƒO
    private bool IsNoDevelopment = false;
    public bool NoDevelpment
    {
        set { IsNoDevelopment = value; }
        get { return IsNoDevelopment; }
    }
}
