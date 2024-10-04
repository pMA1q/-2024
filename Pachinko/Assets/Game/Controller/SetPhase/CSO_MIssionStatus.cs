using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MissionTable", menuName = "Table/MissionTable", order = 1)]
public class CSO_MIssionStatus : ScriptableObject
{
    [Header("�~�b�V�������")]
    public List<MissionTable> infomation;

    
}


[System.Serializable]

public class MissionTable
{
    [Header("�~�b�V������")]
    public string name;  // �J�X�^������ێ�����
    [Header("�~�b�V�������e")]
    public List<Mission> mission;
}

[System.Serializable]
public class Mission
{
    [Header("���o��")]
    public string name;
    [Header("�i�x")]
    public float progress;

    [Header("�e�~�b�V�����̃e�N�X�`���}�e���A��")]
    public Material missionTextureMaterial;

    [Header("���o")]
    public GameObject performance;
}
