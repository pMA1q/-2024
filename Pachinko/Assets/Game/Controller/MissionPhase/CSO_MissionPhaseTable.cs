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

    [Header("各ミッションのテクスチャマテリアル")]
    public Material missionTextureMaterial;

    [Header("演出")]
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