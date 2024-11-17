using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_MissionData : MonoBehaviour
{
    [SerializeField, Header("�v���C���[�X�e�[�^�X")]
    private CSO_PlayerStatus mPlayerStatus;
    public CSO_PlayerStatus PlayerStatus { get{ return mPlayerStatus; } }

    public enum MISSION_TYPE
    {
        COLLECT = 0,    //���W
        SUBJUGATION,//����
        TRAINING,   //�g���[�j���O
    }
    private MISSION_TYPE mMisisonNumber = MISSION_TYPE.COLLECT;
    
    //�~�b�V�����̎�ނ�ݒ�A�擾
    public MISSION_TYPE MissionType
    {
        set { mMisisonNumber = value; }
        get { return mMisisonNumber;  }
    }

    //�����W�t���O
    private bool IsNoDevelopment = false;
    public bool NoDevelpment
    {
        set { IsNoDevelopment = value; }
        get { return IsNoDevelopment; }
    }
        

    private bool IsChoiceSuccess = false;
    //�I���t���O�̎擾
    public bool GetChoiceSuccess() { return IsChoiceSuccess; }
    //�I�𐬌�
    public void ChoiceSuccess(){ IsChoiceSuccess = true; }
    //�I���t���O�����Z�b�g
    public void ChoiceSuccessReset(){ IsChoiceSuccess = false; }

    //����I�o�����m���ɐ키�t���O
    private bool IsBattle = false;
    public bool NextBattleFlag 
    {
        set { IsBattle = value; }
        get { return IsBattle; }
    }


    //�����񂳂ꂽ�~�b�V�����ԍ���ۑ�
    public void SaveMissionContents(int _count, int _contentNum)
    {
        mMissionContentsNums[_count] = _contentNum;
    }

    //�~�b�V�������e
    private int[] mMissionContentsNums = new int[3];
    public int GetMissionContent(int _contentsNum)
    {
        return mMissionContentsNums[_contentsNum];
    }
}
