//---------------------------------
//�����t�F�[�Y�̃e�[�u��
//�S���ҁF����
//---------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SetPhaseTable", menuName = "Table/SetFhaseTable", order = 1)]
public class CSO_SetPhaseTable : ScriptableObject
{
    [Header("�~�b�V�������")]
    public List<SetPhaseTable> infomation;

}


[System.Serializable]

public class SetPhaseTable
{
    [Header("�~�b�V������")]
    public string name;  // �J�X�^������ێ�����
    [Header("�~�b�V�������e")]
    public List<SetPhaseInfomation> mission;
}

[System.Serializable]
public class SetPhaseInfomation
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
