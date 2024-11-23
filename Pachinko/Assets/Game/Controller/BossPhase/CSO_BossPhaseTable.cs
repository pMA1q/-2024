//---------------------------------
//�~�b�V�����t�F�[�Y�̃e�[�u��
//�S���ҁF����
//---------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BossPhaseTable", menuName = "Table/BossFhaseTable", order = 1)]
public class CSO_BossPhaseTable : ScriptableObject
{
    [Header("�~�b�V�������")]
    public List<BossPhaseInfomation> infomation;
}

[System.Serializable]
public class BossPhaseInfomation
{
    [Header("���W���e��")]
    public string name;
    [Header("�������")]
    public WIN_LOST win_lost;
    [Header("�Ē��I�L��")]
    public REPLAY_B replay;
    [Header("�Ē��I��̍��ڔԍ�(�Ē��I�Ȃ��̏ꍇ��0)")]
    public int replayNum;
    [Header("���o�v���n�u")]
    public GameObject performance;
}


//�Ē��I�L��
public enum REPLAY_B
{
    FALSE = 0,//��
    TRUE_P1,
    TRUE_P2,
    TRUE_P3,
    TRUE_P4,
    TRUE_P5,
    
}