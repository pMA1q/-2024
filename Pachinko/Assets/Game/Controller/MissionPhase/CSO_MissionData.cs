using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSO_MissionData : MonoBehaviour
{
    public enum MISSION_TYPE
    {
        COLLECT = 0,    //収集
        SUBJUGATION,//討伐
        TRAINING,   //トレーニング
    }
    private MISSION_TYPE mMisisonNumber = MISSION_TYPE.COLLECT;
    
    //ミッションの種類を設定、取得
    public MISSION_TYPE MissionNumber
    {
        set
        {
            mMisisonNumber = value;
        }
        get
        {
            return mMisisonNumber;
        }
    }
     
}
