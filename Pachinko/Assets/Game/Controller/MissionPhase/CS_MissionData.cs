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
    public MISSION_TYPE MissionType
    {
        set { mMisisonNumber = value; }
        get { return mMisisonNumber;  }
    }

    //無発展フラグ
    private bool IsNoDevelopment = false;
    public bool NoDevelpment
    {
        set { IsNoDevelopment = value; }
        get { return IsNoDevelopment; }
    }
        

    private bool IsChoiceSuccess = false;
    //選択フラグの取得
    public bool GetChoiceSuccess() { return IsChoiceSuccess; }
    //選択成功
    public void ChoiceSuccess(){ IsChoiceSuccess = true; }
    //選択フラグをリセット
    public void ChoiceSuccessReset(){ IsChoiceSuccess = false; }

    //次回的出現時確実に戦うフラグ
    private bool IsBattle = false;
    public bool NextBattleFlag 
    {
        set { IsBattle = value; }
        get { return IsBattle; }
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
