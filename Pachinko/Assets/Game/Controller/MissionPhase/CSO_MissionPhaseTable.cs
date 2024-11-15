//---------------------------------
//ミッションフェーズのテーブル
//担当者：中島
//---------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SetPhaseTable", menuName = "Table/SetFhaseTable", order = 1)]
public class CSO_MissionPhaseTable : ScriptableObject
{
    [Header("ミッション情報")]
    public List<MissionPhaseInfomation> infomation;
}

[System.Serializable]
public class MissionPhaseInfomation
{
    [Header("発展内容名")]
    public string name;
    [Header("当落情報")]
    public WIN_LOST win_lost;
    [Header("再抽選有無")]
    public REPLAY replay;
    [Header("再抽選先の項目番号(再抽選なしの場合は0)")]
    public int replayNum;
    [Header("演出プレハブ")]
    public GameObject performance;
}

//当落情報
public enum WIN_LOST
{
    LOST = 0,//ハズレ
    SMALL_WIN,//小当たり
    MIDDLE_WIN,//中当たり
    BIG_WIN,//大当たり
    RANDOM//ランダム
}

//再抽選有無
public enum REPLAY
{
    FALSE = 0,//無
    TRUE_P1,
    TRUE_P2
    
}