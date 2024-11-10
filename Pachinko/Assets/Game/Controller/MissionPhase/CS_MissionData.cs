using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_MissionData : MonoBehaviour
{
    [SerializeField, Header("プレイヤーステータス")]
    private CSO_PlayerStatus mPlayerStatus;
    public CSO_PlayerStatus PlayerStatus { get{ return mPlayerStatus; } }

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
     
    //抽せんされたミッション番号を保存
    public void SaveMissionContents(int _count, int _contentNum)
    {
        mMissionContentsNums[_count] = _contentNum;
    }

    //ミッション内容
    private int[] mMissionContentsNums = new int[3];
    public int GetMissionContent(int _contentsNum)
    {
        return mMissionContentsNums[_contentsNum];
    }
}
