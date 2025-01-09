using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_RushPhaseData : MonoBehaviour
{
    private bool mJackpot;
    public bool JackPot
    {
        set { mJackpot = value; }
        get { return mJackpot; }
    }
}
