//---------------------------------
//準備フェーズのテーブル
//担当者：中島
//---------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SetPhaseTable", menuName = "Table/SetFhaseTable", order = 1)]
public class CSO_SetPhaseTable : ScriptableObject
{
    [Header("ミッション情報")]
    public List<SetPhaseTable> infomation;

}


[System.Serializable]

public class SetPhaseTable
{
    [Header("ミッション名")]
    public string name;  // カスタム名を保持する
    [Header("ミッション内容")]
    public List<SetPhaseInfomation> mission;
}

[System.Serializable]
public class SetPhaseInfomation
{
    [Header("演出名")]
    public string name;
    [Header("進度")]
    public float progress;

    [Header("各ミッションのテクスチャマテリアル")]
    public Material missionTextureMaterial;

    [Header("演出")]
    public GameObject performance;
}
