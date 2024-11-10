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
    public MISSION_TYPE MissionNumber
    {
        set
        {
            mMisisonNumber = value;
        }
        get
        {
            return mMisisonNumber;
        }
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
