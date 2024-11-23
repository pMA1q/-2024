//---------------------------------
//ミッションフェーズのテーブル
//担当者：中島
//---------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BossPhaseTable", menuName = "Table/BossFhaseTable", order = 1)]
public class CSO_BossPhaseTable : ScriptableObject
{
    [Header("ミッション情報")]
    public List<BossPhaseInfomation> infomation;
}

[System.Serializable]
public class BossPhaseInfomation
{
    [Header("発展内容名")]
    public string name;
    [Header("当落情報")]
    public WIN_LOST win_lost;
    [Header("再抽選有無")]
    public REPLAY_B replay;
    [Header("再抽選先の項目番号(再抽選なしの場合は0)")]
    public int replayNum;
    [Header("演出プレハブ")]
    public GameObject performance;
}


//再抽選有無
public enum REPLAY_B
{
    FALSE = 0,//無
    TRUE_P1,
    TRUE_P2,
    TRUE_P3,
    TRUE_P4,
    TRUE_P5,
    
}