//---------------------------------
//�~�b�V�����t�F�[�Y�̃e�[�u��
//�S���ҁF����
//---------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SetPhaseTable", menuName = "Table/SetFhaseTable", order = 1)]
public class CSO_MissionPhaseTable : ScriptableObject
{
    [Header("�~�b�V�������")]
    public List<MissionPhaseInfomation> infomation;


}

[System.Serializable]
public class MissionPhaseInfomation
{
    [Header("���W���e��")]
    public string name;
    [Header("�������")]
    public WIN_LOST win_lost;

    [Header("�e�~�b�V�����̃e�N�X�`���}�e���A��")]
    public Material missionTextureMaterial;

    [Header("���o")]
    public GameObject performance;
}

//�������
public enum WIN_LOST
{
    LOST = 0,//�n�Y��
    SMALL_WIN,//��������
    MIDDLE_WIN,//��������
    BIG_WIN,//�哖����
    RANDOM//�����_��
}