using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CS_BonusPhaseData : MonoBehaviour
{
    private int mRoundCount = 1;
    public int RoundCount
    {
        set { mRoundCount = value; }
        get { return mRoundCount; }
    }

    [NonSerialized]
    public int MaxRound;

    private bool isBonusFinish = false;
    public bool IsBonusFinish
    {
        set { isBonusFinish = value; }
        get { return isBonusFinish; }
    }
    
}
